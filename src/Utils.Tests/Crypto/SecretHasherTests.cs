using System;
using NUnit.Framework;

namespace DavidLievrouw.Utils.Crypto {
  [TestFixture]
  public class SecretHasherTests {
    SecretHasher _sut;

    [SetUp]
    public void SetUp() {
      _sut = new SecretHasher();
    }

    public class CreateHashForSecret : SecretHasherTests {
      [Test]
      public void GivenNullSecret_Throws() {
        Assert.Throws<ArgumentNullException>(() => _sut.CreateHashForSecret(null));
      }

      [Test]
      public void GivenEmptySecret_DoesNotThrow_CreatesHash() {
        var actual = _sut.CreateHashForSecret(string.Empty);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.Not.Empty);
      }

      [Test]
      public void CreatesRandomHashForSecret() {
        var actual = _sut.CreateHashForSecret("TheSecret");
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.Not.Empty);
      }

      [Test]
      public void DoesNotCreateDuplicateHashesForSameSecrets() {
        var actual1 = _sut.CreateHashForSecret("TheSecret");
        var actual2 = _sut.CreateHashForSecret("TheSecret");
        Assert.That(actual1, Is.Not.Null);
        Assert.That(actual1, Is.Not.Empty);
        Assert.That(actual2, Is.Not.Null);
        Assert.That(actual2, Is.Not.Empty);
        Assert.That(actual1, Is.Not.EqualTo(actual2));
      }
    }

    public class ValidateHashForSecret : SecretHasherTests {
      [Test]
      public void GivenNullSecret_Throws() {
        Assert.Throws<ArgumentNullException>(() => _sut.ValidateHashForSecret(null, "somehash"));
      }

      [Test]
      public void GivenNullHash_Throws() {
        Assert.Throws<ArgumentNullException>(() => _sut.ValidateHashForSecret("somesecret", null));
      }

      [Test]
      public void GivenGoodHash_ReturnsTrue() {
        const string secret = "MyDirtyLittleSecret";
        var goodHash = _sut.CreateHashForSecret(secret);
        var actual = _sut.ValidateHashForSecret(secret, goodHash);
        Assert.That(actual, Is.True);
      }

      [Test]
      public void GivenBadHash_ReturnsFalse() {
        const string badHash = "abc123";
        var actual = _sut.ValidateHashForSecret("TheSecret", badHash);
        Assert.That(actual, Is.False);
      }
    }
  }
}