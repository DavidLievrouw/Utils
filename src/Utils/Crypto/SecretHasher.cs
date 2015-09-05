using System;
using System.Security.Cryptography;

namespace DavidLievrouw.Utils.Crypto {
  public class SecretHasher : ISecretHasher {
    const int SaltBytes = 24;
    const int HashBytes = 24;
    const int Pbkdf2Iterations = 1000;

    const int IterationIndex = 1;
    const int SaltIndex = 2;
    const int Pbkdf2Index = 3;

    public string CreateHashForSecret(string secret) {
      var csprng = new RNGCryptoServiceProvider();
      var salt = new byte[SaltBytes];
      csprng.GetBytes(salt);

      var hash = PBKDF2(secret, salt, Pbkdf2Iterations, HashBytes);
      return "sha1:" + Pbkdf2Iterations + ":" +
             Convert.ToBase64String(salt) + ":" +
             Convert.ToBase64String(hash);
    }

    public bool ValidateHashForSecret(string secret, string goodHash) {
      char[] delimiter = {':'};
      var split = goodHash.Split(delimiter);
      var iterations = int.Parse(split[IterationIndex]);
      var salt = Convert.FromBase64String(split[SaltIndex]);
      var hash = Convert.FromBase64String(split[Pbkdf2Index]);

      var testHash = PBKDF2(secret, salt, iterations, hash.Length);
      return SlowEquals(hash, testHash);
    }

    static bool SlowEquals(byte[] a, byte[] b) {
      var diff = (uint) a.Length ^ (uint) b.Length;
      for (var i = 0; i < a.Length && i < b.Length; i++) {
        diff |= (uint) (a[i] ^ b[i]);
      }
      return diff == 0;
    }

    static byte[] PBKDF2(string secret, byte[] salt, int iterations, int outputBytes) {
      var pbkdf2 = new Rfc2898DeriveBytes(secret, salt) {IterationCount = iterations};
      return pbkdf2.GetBytes(outputBytes);
    }
  }
}