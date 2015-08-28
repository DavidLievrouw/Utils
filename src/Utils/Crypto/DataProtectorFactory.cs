namespace DavidLievrouw.Utils.Crypto {
  public class DataProtectorFactory : IDataProtectorFactory {
    public IDataProtector Create(byte[] entropy) {
      return new DataProtector(entropy);
    }
  }
}