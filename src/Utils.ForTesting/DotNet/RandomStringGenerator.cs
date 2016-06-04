using System;
using System.Linq;
using System.Security.Cryptography;

namespace DavidLievrouw.Utils.ForTesting.DotNet {
  public class RandomStringGenerator : IRandomStringGenerator {
    readonly int _length;
    readonly char[] _characterRange;

    public RandomStringGenerator(int length, char[] characterRange = null) {
      if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
      _length = length;
      _characterRange = characterRange ?? "AZERTYUIPQSDFGHJKLMWXCVBNOazertyuipqsdfghjklmwxcvbno0123456789 -/*+$%@#^&*()'~|<>,.".ToCharArray();
      if (!_characterRange.Any()) throw new ArgumentOutOfRangeException(nameof(characterRange));
    }

    public string GenerateString() {
      var csrngProvider = new RNGCryptoServiceProvider();
      var indices = new byte[_length];
      csrngProvider.GetBytes(indices);
      return new string(indices.Select(i => _characterRange[i%_characterRange.Length]).ToArray());
    }
  }
}