using System.Collections.Generic;
using System.Linq;

namespace DavidLievrouw.Utils {
  public static partial class ExtensionsForT {
    public static IEnumerable<T> ToEnumerable<T>(this T instance) {
      return instance.IsNullOrDefault()
        ? Enumerable.Empty<T>()
        : new[] {instance};
    }
  }
}