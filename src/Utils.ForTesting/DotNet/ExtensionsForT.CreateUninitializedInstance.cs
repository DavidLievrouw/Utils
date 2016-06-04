using System.Runtime.Serialization;

namespace DavidLievrouw.Utils.ForTesting.DotNet {
  public static partial class ExtensionsForT {
    public static T CreateUninitializedInstance<T>(this T reference) where T : class {
      return (T)FormatterServices.GetUninitializedObject(typeof(T));
    }
  }
}