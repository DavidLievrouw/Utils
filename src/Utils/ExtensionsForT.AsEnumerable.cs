using System.Collections.Generic;
using System.Linq;

namespace DavidLievrouw.Utils {
  public static partial class ExtensionsForT {
    public static IEnumerable<T> AsEnumerable<T>(this T instance) {
      return instance.IsNullOrDefault()
        ? Enumerable.Empty<T>()
        : new[] {instance};
    }
  }
}