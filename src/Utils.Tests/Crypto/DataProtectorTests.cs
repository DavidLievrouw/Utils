﻿using System;
using System.Linq;
using System.Security.Cryptography;
using FakeItEasy;
using NUnit.Framework;

namespace DavidLievrouw.Utils.Crypto {
  [TestFixture]
  public class DataProtectorTests {
    byte[] _entropy;
    byte[] _userData;
    DataProtector _sut;

    [SetUp]
    public virtual void SetUp() {
      _entropy = new byte[] {1, 2, 3};
      _userData = new byte[] {4, 5, 6};
      _sut = new DataProtector(_entropy);
    }

    [Test]
    public void ConstructWithNullEntropy_IsAllowed() {
      var actual = new DataProtector(null);
      Assert.That(actual, Is.Not.Null);
    }

    [Test]
    public void DataProtectorWithDifferentEntropy_CannotDecrypt() {
      var entropy2 = new byte[] {7, 8, 9, 10};
      var protector2 = new DataProtector(entropy2);

      var cypher = _sut.Protect(_userData);

      Assert.Throws<CryptographicException>(() => protector2.Unprotect(cypher));
    }

    public class Protect : DataProtectorTests {
      [Test]
      public void GivenUserDataIsNull_ReturnsNull() {
        var actual = _sut.Protect(null);
        Assert.That(actual, Is.Null);
      }

      [Test]
      public void GivenValidUserData_ReturnsProtectedData() {
        var actual = _sut.Protect(_userData);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.SequenceEqual(_userData), Is.False);
      }
    }

    public class Unprotect : DataProtectorTests {
      [Test]
      public void GivenCypherIsNull_ReturnsNull() {
        var actual = _sut.Unprotect(null);
        Assert.That(actual, Is.Null);
      }

      [Test]
      public void GivenValidCypher_ReturnsOriginalData() {
        var validCypher = _sut.Protect(_userData);
        var actual = _sut.Unprotect(validCypher);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.SequenceEqual(validCypher), Is.False);
        Assert.That(actual.SequenceEqual(_userData), Is.True);
      }
    }
  }
}