using System;

namespace DavidLievrouw.Utils {
  public static partial class ExtensionsForT {
    public static TResult Get<TObject, TResult>(this TObject @this, Func<TObject, TResult> getter) {
      return @this != null
        ? getter(@this)
        : default(TResult);
    }
  }
}