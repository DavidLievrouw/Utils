using System.Collections.Generic;
using System.Linq;

namespace DavidLievrouw.Utils {
  public static class GenericExtensions {
    public static bool IsNullOrDefault<T>(this T reference) {
      return (EqualityComparer<T>.Default.Equals(reference, default(T)));
    }

    public static IEnumerable<T> ToEnumerable<T>(this T instance) {
      return instance.IsNullOrDefault()
        ? Enumerable.Empty<T>()
        : new[] { instance };
    }
  }
}