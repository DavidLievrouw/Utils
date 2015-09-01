using System;
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
        : new[] {instance};
    }

    public static TResult Get<TObject, TResult>(this TObject @this, Func<TObject, TResult> getter) {
      return @this != null
        ? getter(@this)
        : default(TResult);
    }

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