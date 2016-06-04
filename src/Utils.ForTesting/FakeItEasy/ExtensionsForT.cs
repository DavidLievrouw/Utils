using FakeItEasy;

namespace DavidLievrouw.Utils.ForTesting.FakeItEasy {
  public static class ExtensionsForT {
    public static T Fake<T>(this T reference) {
      return A.Fake<T>();
    }

    public static T Dummy<T>(this T reference) {
      return A.Dummy<T>();
    }
  }
}