using System;

namespace DavidLievrouw.Utils {
  public static partial class ExtensionsForT {
    public static TResult DefaultIf<TResult>(this TResult input, TResult value) {
      return DefaultIf(input, value, x => x.IsNullOrDefault());
    }

    public static TResult DefaultIf<TResult>(this TResult input, TResult value, Predicate<TResult> predicate) {
      if (predicate == null) throw new ArgumentNullException("predicate");

      return predicate(input)
        ? value
        : input;
    }
  }
}