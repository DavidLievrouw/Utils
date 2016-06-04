using System.Collections.Generic;

namespace DavidLievrouw.Utils {
  public static partial class ExtensionsForT {
    public static bool IsNullOrDefault<T>(this T reference) {
      return (EqualityComparer<T>.Default.Equals(reference, default(T)));
    }
  }
}