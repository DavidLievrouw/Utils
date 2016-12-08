using System;
using System.Collections.Generic;
using System.Text;
using DavidLievrouw.Utils.ForTesting.DotNet;
using DavidLievrouw.Utils.ForTesting.FakeItEasy;
using FakeItEasy;
using NUnit.Framework;

namespace DavidLievrouw.Utils.Crypto {
  [TestFixture]
  public class LocalMachineScopeStringEncryptorTests {
    IEntropyCreator _entropyCreator;
    IDataProtectorFactory _dataProtectorFactory;
    LocalMachineScopeStringEncryptor _sut;
    IDataProtector _dataProtector;

    [SetUp]
    public void SetUp() {
      _entropyCreator = _entropyCreator.Fake();
      _dataProtectorFactory = _dataProtectorFactory.Fake();
      _sut = new LocalMachineScopeStringEncryptor(_entropyCreator, _dataProtectorFactory);

      _dataProtector = _dataProtector.Fake();
    }

    [Test]
    public void ConstructorTests() {
      Assert.That(_sut.NoDependenciesAreOptional());
    }

    public class Encrypt : LocalMachineScopeStringEncryptorTests {
      [Test]
      public void WhenNoInputStringIsGiven_Throws() {
        Assert.Throws<ArgumentNullException>(() => _sut.Encrypt(null));
      }

      [Test]
      public void WhenEmptyInputStringIsGiven_Throws() {
        Assert.Throws<ArgumentNullException>(() => _sut.Encrypt(string.Empty));
      }

      [Test]
      public void WhenWhitespaceInputStringIsGiven_DoesNotThrow() {
        Assert.DoesNotThrow(() => _sut.Encrypt(" "));
      }

      [Test]
      public void GivenNoPurposes_ProtectsUserData_UsingEntropy() {
        const string stringToEncrypt = "The string to encrypt!";
        string[] purposes = null;

        var entropy = new byte[] {1, 2, 3};
        ConfigureEntropyCreator_ToReturn(purposes, entropy);

        ConfigureDataProtectorCreator_ToReturn(entropy, _dataProtector);

        var expectedUserData = Encoding.UTF8.GetBytes(stringToEncrypt);
        var expectedCypher = new byte[] {4, 5, 6, 7};
        ConfigureDataProtector_Protect_ToReturn(expectedUserData, expectedCypher);

        var expectedResult = Convert.ToBase64String(expectedCypher);

        var actual = _sut.Encrypt(stringToEncrypt, purposes);

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.EqualTo(expectedResult));
        A.CallTo(() => _entropyCreator.CreateEntropy(A<IEnumerable<string>>.That.IsNull()))
         .MustHaveHappened();
        A.CallTo(() => _dataProtectorFactory.Create(A<byte[]>.That.IsSameSequenceAs(entropy)))
         .MustHaveHappened();
        A.CallTo(() => _dataProtector.Protect(A<byte[]>.That.IsSameSequenceAs(expectedUserData)))
         .MustHaveHappened();
      }

      [Test]
      public void GivenInputStringAndPurposes_ProtectsUserData_UsingEntropy_AndPurposes() {
        const string stringToEncrypt = "The string to encrypt!";
        var purposes = new[] {"My", "Purposes"};

        var entropy = new byte[] {1, 2, 3};
        ConfigureEntropyCreator_ToReturn(purposes, entropy);

        ConfigureDataProtectorCreator_ToReturn(entropy, _dataProtector);

        var expectedUserData = Encoding.UTF8.GetBytes(stringToEncrypt);
        var expectedCypher = new byte[] {4, 5, 6, 7};
        ConfigureDataProtector_Protect_ToReturn(expectedUserData, expectedCypher);

        var expectedResult = Convert.ToBase64String(expectedCypher);

        var actual = _sut.Encrypt(stringToEncrypt, purposes);

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.EqualTo(expectedResult));
        A.CallTo(() => _entropyCreator.CreateEntropy(A<IEnumerable<string>>.That.IsSameSequenceAs(purposes)))
         .MustHaveHappened();
        A.CallTo(() => _dataProtectorFactory.Create(A<byte[]>.That.IsSameSequenceAs(entropy)))
         .MustHaveHappened();
        A.CallTo(() => _dataProtector.Protect(A<byte[]>.That.IsSameSequenceAs(expectedUserData)))
         .MustHaveHappened();
      }

      void ConfigureDataProtector_Protect_ToReturn(byte[] userData, byte[] cypher) {
        A.CallTo(() => _dataProtector.Protect(A<byte[]>.That.IsSameSequenceAs(userData)))
         .Returns(cypher);
      }
    }

    public class Decrypt : LocalMachineScopeStringEncryptorTests {
      [Test]
      public void WhenNoInputStringIsGiven_Throws() {
        Assert.Throws<ArgumentNullException>(() => _sut.Decrypt(null));
      }

      [Test]
      public void WhenEmptyInputStringIsGiven_Throws() {
        Assert.Throws<ArgumentNullException>(() => _sut.Decrypt(string.Empty));
      }

      [Test]
      public void WhenWhitespaceInputStringIsGiven_DoesNotThrow() {
        Assert.DoesNotThrow(() => _sut.Decrypt(" "));
      }

      [Test]
      public void GivenNoPurposes_UnprotectsUserData_UsingEntropy() {
        var stringToDecrypt = Convert.ToBase64String(Encoding.UTF8.GetBytes("CYPHERSTRING!"));
        string[] purposes = null;

        var entropy = new byte[] {1, 2, 3};
        ConfigureEntropyCreator_ToReturn(purposes, entropy);

        ConfigureDataProtectorCreator_ToReturn(entropy, _dataProtector);

        var cypherToDecrypt = Convert.FromBase64String(stringToDecrypt);
        var expectedUserData = Encoding.UTF8.GetBytes("UserDataString!");
        ConfigureDataProtector_Unprotect_ToReturn(cypherToDecrypt, expectedUserData);

        var expectedResult = Encoding.UTF8.GetString(expectedUserData);

        var actual = _sut.Decrypt(stringToDecrypt, purposes);

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.EqualTo(expectedResult));
        A.CallTo(() => _entropyCreator.CreateEntropy(A<IEnumerable<string>>.That.IsNull()))
         .MustHaveHappened();
        A.CallTo(() => _dataProtectorFactory.Create(A<byte[]>.That.IsSameSequenceAs(entropy)))
         .MustHaveHappened();
        A.CallTo(() => _dataProtector.Unprotect(A<byte[]>.That.IsSameSequenceAs(cypherToDecrypt)))
         .MustHaveHappened();
      }

      [Test]
      public void GivenInputStringAndPurposes_UnprotectsUserData_UsingEntropy_AndPurposes() {
        var stringToDecrypt = Convert.ToBase64String(Encoding.UTF8.GetBytes("CYPHERSTRING!"));
        var purposes = new[] {"My", "Purposes"};

        var entropy = new byte[] {1, 2, 3};
        ConfigureEntropyCreator_ToReturn(purposes, entropy);

        ConfigureDataProtectorCreator_ToReturn(entropy, _dataProtector);

        var cypherToDecrypt = Convert.FromBase64String(stringToDecrypt);
        var expectedUserData = Encoding.UTF8.GetBytes("UserDataString!");
        ConfigureDataProtector_Unprotect_ToReturn(cypherToDecrypt, expectedUserData);

        var expectedResult = Encoding.UTF8.GetString(expectedUserData);

        var actual = _sut.Decrypt(stringToDecrypt, purposes);

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.EqualTo(expectedResult));
        A.CallTo(() => _entropyCreator.CreateEntropy(A<IEnumerable<string>>.That.IsSameSequenceAs(purposes)))
         .MustHaveHappened();
        A.CallTo(() => _dataProtectorFactory.Create(A<byte[]>.That.IsSameSequenceAs(entropy)))
         .MustHaveHappened();
        A.CallTo(() => _dataProtector.Unprotect(A<byte[]>.That.IsSameSequenceAs(cypherToDecrypt)))
         .MustHaveHappened();
      }

      void ConfigureDataProtector_Unprotect_ToReturn(byte[] cypher, byte[] userData) {
        A.CallTo(() => _dataProtector.Unprotect(A<byte[]>.That.IsSameSequenceAs(cypher)))
         .Returns(userData);
      }
    }

    void ConfigureEntropyCreator_ToReturn(IEnumerable<string> purposes, byte[] entropy) {
      A.CallTo(() => _entropyCreator.CreateEntropy(purposes))
       .Returns(entropy);
    }

    void ConfigureDataProtectorCreator_ToReturn(byte[] entropy, IDataProtector dataProtector) {
      A.CallTo(() => _dataProtectorFactory.Create(A<byte[]>.That.IsSameSequenceAs(entropy)))
       .Returns(dataProtector);
    }
  }
}