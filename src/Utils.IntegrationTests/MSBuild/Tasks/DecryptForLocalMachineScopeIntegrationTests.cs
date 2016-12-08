﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FakeItEasy;
using DavidLievrouw.Utils.Crypto;

namespace DavidLievrouw.Utils.MSBuild.Tasks {
  [TestFixture, Category("Integration")]
  public class DecryptForLocalMachineScopeIntegrationTests {
    ITaskLogger _taskLogger;
    DecryptForLocalMachineScope _sut;

    [SetUp]
    public void SetUp() {
      _sut = new DecryptForLocalMachineScope();

      _taskLogger = A.Fake<ITaskLogger>();
      _sut.Logger = _taskLogger;
    }

    [Test]
    public void WhenNoInputStringIsGiven_Throws() {
      Assert.Throws<AggregateException>(() => _sut.Execute());
    }

    [Test]
    public void WhenEmptyInputStringIsGiven_Throws() {
      _sut.StringToDecrypt = string.Empty;
      Assert.Throws<AggregateException>(() => _sut.Execute());
    }

    [Test]
    public void WhenWhitespaceInputStringIsGiven_Throws() {
      _sut.StringToDecrypt = " ";
      Assert.Throws<AggregateException>(() => _sut.Execute());
    }

    [Test]
    public void GivenNoPurposes_EncryptsCorrectly() {
      const string expected = "The decrypted string!! $µ£";
      var encryptedString = ManuallyEncrypt(expected);
      _sut.StringToDecrypt = encryptedString;
      _sut.Purposes = null;

      var result = _sut.Execute();
      var actual = _sut.DecryptedString;

      Assert.That(result, Is.True);
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void IgnoresNullEmptyOrWhitespacePurposes() {
      const string expected = "The decrypted string!! $µ£";
      var encryptedString = ManuallyEncrypt(expected, new[] {"David", "Lievrouw"});
      _sut.StringToDecrypt = encryptedString;

      _sut.Purposes = new[] {"David", null, "Lievrouw", string.Empty, " "};
      _sut.Execute();
      var actual1 = _sut.DecryptedString;
      _sut.Purposes = new[] {"David", "Lievrouw"};
      _sut.Execute();
      var actual2 = _sut.DecryptedString;

      Assert.That(actual1, Is.EqualTo(actual2));
    }

    [Test]
    public void GivenStringWithPurposes_Encrypts() {
      const string expected = "The decrypted string!! $µ£";
      var encryptedString = ManuallyEncrypt(expected, new[] {"David", "Lievrouw"});
      _sut.StringToDecrypt = encryptedString;
      _sut.Purposes = new[] {"David", "Lievrouw"};

      var result = _sut.Execute();
      var actual = _sut.DecryptedString;

      Assert.That(result, Is.True);
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual, Is.EqualTo(expected));
    }

    static string ManuallyEncrypt(string stringtoEncrypt, IEnumerable<string> purposes = null) {
      var entropyCreator = new EntropyCreator();
      var entropy = entropyCreator.CreateEntropy(purposes);
      var protector = new DataProtector(entropy);

      var userData = Encoding.UTF8.GetBytes(stringtoEncrypt);
      var cypher = protector.Protect(userData);

      return Convert.ToBase64String(cypher);
    }
  }
}