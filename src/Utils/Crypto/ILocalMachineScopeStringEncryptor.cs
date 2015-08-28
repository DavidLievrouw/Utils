using System.Collections.Generic;

namespace DavidLievrouw.Utils.Crypto {
  public interface ILocalMachineScopeStringEncryptor {
    string Encrypt(string inputToEncrypt, IEnumerable<string> purposes = null);
    string Decrypt(string inputToDecrypt, IEnumerable<string> purposes = null);
  }
}