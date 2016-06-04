using System;
using System.Collections.Generic;

namespace DavidLievrouw.Utils {
  public static class ExtensionsForEnumerableOfT {
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
      foreach (var element in source) {
        action(element);
      }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action) {
      var index = 0;
      foreach (var element in source) {
        action(element, index++);
      }
    }
  }
}