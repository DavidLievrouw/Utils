using System;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Types;

namespace DavidLievrouw.Utils.ForTesting.FluentAssertions {
  public static class ExtensionsForConstructorInfoAssertions {
    public static AndConstraint<ConstructorInfoAssertions> NotHaveOptionalParameters(
      this ConstructorInfoAssertions constructorInfoAssertions) {
      var ctor = constructorInfoAssertions.Subject;
      var dummyMethod = typeof(A).GetMethod("Dummy");
      var ctorParameters = ctor.GetParameters().ToArray();
      var ctorArguments =
        ctorParameters.Select(p => dummyMethod.MakeGenericMethod(p.ParameterType).Invoke(null, null)).ToArray();

      for (var i = 0; i < ctorArguments.Length; i++) {
        try {
          var args = new object[ctorArguments.Length];
          Array.Copy(ctorArguments, args, ctorArguments.Length);
          args[i] = null;
          ctor.Invoke(args);
          Execute.Assertion.ForCondition(false)
            .BecauseOf("because passing null should throw an ArgumentNullException")
            .FailWith(
              "Expected the constructor of {0} to throw an ArgumentNullException when passing in null for parameter {1} with name {2} and type {3}{reason} but no exceptions were thrown.",
              ctor.DeclaringType, i, ctorParameters[i].Name, ctorParameters[i].ParameterType);
        } catch (TargetInvocationException ex) {
          Execute.Assertion.ForCondition(ex.InnerException is ArgumentNullException)
            .BecauseOf("because passing null to the constructor should throw an ArgumentNullException")
            .FailWith(
              "Expected the constructor of {0} to throw an {1}{reason} but it threw an {2} when passing in null for parameter {3} with name {4} and type {5}.",
              ctor.DeclaringType, typeof(ArgumentNullException), ex.InnerException.GetType(), i,
              ctorParameters[i].Name, ctorParameters[i].ParameterType);
        } catch (ArgumentNullException) {
          // Ctor fails with ArgumentNullException when null is passed, as expected
        }
      }
      return new AndConstraint<ConstructorInfoAssertions>(constructorInfoAssertions);
    }
  }
}