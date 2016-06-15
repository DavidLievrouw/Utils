using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace DavidLievrouw.Utils.ForTesting.NUnit {
  public static class Has {
    public static IResolveConstraint SamePropertyValuesAs(object expected) {
      return new SamePropertyValuesConstraint(expected);
    }

    public static IResolveConstraint SamePropertyValuesAs(object expected, IEnumerable<string> membersToIgnore) {
      return new SamePropertyValuesConstraint(expected, membersToIgnore);
    }

    public static IResolveConstraint SamePropertyValuesAs(object expected, bool ignoreCollectionOrder) {
      return new SamePropertyValuesConstraint(expected, ignoreCollectionOrder);
    }

    public static IResolveConstraint SamePropertyValuesAs(object expected, bool ignoreCollectionOrder, IEnumerable<string> membersToIgnore) {
      return new SamePropertyValuesConstraint(expected, ignoreCollectionOrder, membersToIgnore);
    }

    public static ConstraintExpression All => global::NUnit.Framework.Has.All;
    public static ResolvableConstraintExpression Count => global::NUnit.Framework.Has.Count;
    public static ResolvableConstraintExpression InnerException => global::NUnit.Framework.Has.InnerException;
    public static ResolvableConstraintExpression Length => global::NUnit.Framework.Has.Length;
    public static ResolvableConstraintExpression Message => global::NUnit.Framework.Has.Message;
    public static ConstraintExpression No => global::NUnit.Framework.Has.No;
    public static ConstraintExpression None => global::NUnit.Framework.Has.None;
    public static ConstraintExpression Some => global::NUnit.Framework.Has.Some;

    public static ResolvableConstraintExpression Attribute(Type expectedType) {
      return global::NUnit.Framework.Has.Attribute(expectedType);
    }

    public static ResolvableConstraintExpression Attribute<T>() {
      return global::NUnit.Framework.Has.Attribute<T>();
    }

    public static ConstraintExpression Exactly(int expectedCount) {
      return global::NUnit.Framework.Has.Exactly(expectedCount);
    }

    public static CollectionContainsConstraint Member(object expected) {
      return global::NUnit.Framework.Has.Member(expected);
    }

    public static ResolvableConstraintExpression Property(string name) {
      return global::NUnit.Framework.Has.Property(name);
    }
  }
}