namespace DavidLievrouw.Utils.Crypto {
  public interface IDataProtector {
    byte[] Protect(byte[] userData);
    byte[] Unprotect(byte[] userData);
  }
}