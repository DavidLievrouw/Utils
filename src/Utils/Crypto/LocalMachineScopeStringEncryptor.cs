using System;
using System.Collections.Generic;
using System.Text;

namespace DavidLievrouw.Utils.Crypto {
  public class LocalMachineScopeStringEncryptor : ILocalMachineScopeStringEncryptor {
    readonly IEntropyCreator _entropyCreator;
    readonly IDataProtectorFactory _dataProtectorFactory;

    public LocalMachineScopeStringEncryptor(IEntropyCreator entropyCreator, IDataProtectorFactory dataProtectorFactory) {
      if (entropyCreator == null) throw new ArgumentNullException("entropyCreator");
      if (dataProtectorFactory == null) throw new ArgumentNullException("dataProtectorFactory");
      _entropyCreator = entropyCreator;
      _dataProtectorFactory = dataProtectorFactory;
    }

    public string Encrypt(string inputToEncrypt, IEnumerable<string> purposes = null) {
      if (string.IsNullOrEmpty(inputToEncrypt)) throw new ArgumentNullException("inputToEncrypt");

      var entropy = _entropyCreator.CreateEntropy(purposes);
      var dataProtector = _dataProtectorFactory.Create(entropy);

      var userData = Encoding.UTF8.GetBytes(inputToEncrypt);
      var cypher = dataProtector.Protect(userData);

      return Convert.ToBase64String(cypher);
    }

    public string Decrypt(string inputToDecrypt, IEnumerable<string> purposes = null) {
      if (string.IsNullOrEmpty(inputToDecrypt)) throw new ArgumentNullException("inputToDecrypt");

      var entropy = _entropyCreator.CreateEntropy(purposes);
      var dataProtector = _dataProtectorFactory.Create(entropy);

      var cypher = Convert.FromBase64String(inputToDecrypt);
      var userData = dataProtector.Unprotect(cypher);

      return Encoding.UTF8.GetString(userData);
    }
  }
}