using FluentAssertions;
using FluentAssertions.Primitives;

namespace DavidLievrouw.Utils.ForTesting.FluentAssertions {
  public static class ExtensionsForReferenceTypeAssertions {
    public static AndConstraint<TAssertions> HaveExactlyOneConstructorWithoutOptionalParameters<TSubject, TAssertions>(this ReferenceTypeAssertions<TSubject, TAssertions> referenceTypeAssertions)
      where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions> {
      referenceTypeAssertions.Subject.GetType().Should().HaveExactlyOneConstructorWithoutOptionalParameters();
      return new AndConstraint<TAssertions>((TAssertions)referenceTypeAssertions);
    }
  }
}