﻿using DavidLievrouw.Utils.ForTesting.FluentAssertions;
using FluentAssertions;
using NUnit.Framework;

namespace DavidLievrouw.Utils.Crypto {
  [TestFixture]
  public class DataProtectorFactoryTests {
    DataProtectorFactory _sut;

    [SetUp]
    public void SetUp() {
      _sut = new DataProtectorFactory();
    }

    [Test]
    public void ConstructorTests() {
      _sut.Should().HaveExactlyOneConstructorWithoutOptionalParameters();
    }

    [Test]
    public void CreatesNewDataProtector() {
      var entropy = new byte[] {1, 2, 3};
      var actual = _sut.Create(entropy);

      Assert.That(actual, Is.Not.Null);
      Assert.That(actual, Is.InstanceOf<DataProtector>());
    }
  }
}