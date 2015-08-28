using System.Collections.Generic;

namespace DavidLievrouw.Utils.Crypto {
  public interface IEntropyCreator {
    byte[] CreateEntropy(IEnumerable<string> purposes);
  }
}