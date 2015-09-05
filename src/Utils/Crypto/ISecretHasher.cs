namespace DavidLievrouw.Utils.Crypto {
  public interface ISecretHasher {
    string CreateHashForSecret(string secret);
    bool ValidateHashForSecret(string secret, string goodHash);
  }
}