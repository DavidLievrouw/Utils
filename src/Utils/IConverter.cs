namespace DavidLievrouw.Utils {
  public interface IConverter<T1, T2> {
    T2 Convert(T1 code);
    T1 Convert(T2 status);
  }
}