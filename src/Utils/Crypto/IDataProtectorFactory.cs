namespace DavidLievrouw.Utils.Crypto {
  public interface IDataProtectorFactory {
    IDataProtector Create(byte[] entropy);
  }
}