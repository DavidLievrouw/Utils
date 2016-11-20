using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace DavidLievrouw.Utils.ForTesting.NUnit {
  public static class Is {
    public static IResolveConstraint SameCollectionAs<T>(IEnumerable<T> expected) {
      return new SameCollectionAsConstraint<T>(expected);
    }

    public static IResolveConstraint SameCollectionAs<T>(IEnumerable<T> expected, IEnumerable<string> membersToIgnore) {
      return new SameCollectionAsConstraint<T>(expected, membersToIgnore);
    }

    public static ConstraintExpression All => global::NUnit.Framework.Is.All;
    public static BinarySerializableConstraint BinarySerializable => global::NUnit.Framework.Is.BinarySerializable;
    public static EmptyConstraint Empty => global::NUnit.Framework.Is.Empty;
    public static FalseConstraint False => global::NUnit.Framework.Is.False;
    public static NaNConstraint NaN => global::NUnit.Framework.Is.NaN;
    public static LessThanConstraint Negative => global::NUnit.Framework.Is.Negative;
    public static ConstraintExpression Not => global::NUnit.Framework.Is.Not;
    public static NullConstraint Null => global::NUnit.Framework.Is.Null;
    public static CollectionOrderedConstraint Ordered => global::NUnit.Framework.Is.Ordered;
    public static GreaterThanConstraint Positive => global::NUnit.Framework.Is.Positive;
    public static TrueConstraint True => global::NUnit.Framework.Is.True;
    public static UniqueItemsConstraint Unique => global::NUnit.Framework.Is.Unique;
    public static XmlSerializableConstraint XmlSerializable => global::NUnit.Framework.Is.XmlSerializable;
    public static EqualConstraint Zero => global::NUnit.Framework.Is.Zero;

    public static AssignableFromConstraint AssignableFrom(Type expectedType) {
      return global::NUnit.Framework.Is.AssignableFrom(expectedType);
    }

    public static AssignableFromConstraint AssignableFrom<TExpected>() {
      return global::NUnit.Framework.Is.AssignableFrom<TExpected>();
    }

    public static AssignableToConstraint AssignableTo(Type expectedType) {
      return global::NUnit.Framework.Is.AssignableTo(expectedType);
    }

    public static AssignableToConstraint AssignableTo<TExpected>() {
      return global::NUnit.Framework.Is.AssignableTo<TExpected>();
    }

    public static GreaterThanOrEqualConstraint AtLeast(object expected) {
      return global::NUnit.Framework.Is.AtLeast(expected);
    }

    public static LessThanOrEqualConstraint AtMost(object expected) {
      return global::NUnit.Framework.Is.AtMost(expected);
    }

    public static EqualConstraint EqualTo(object expected) {
      return global::NUnit.Framework.Is.EqualTo(expected);
    }

    public static CollectionEquivalentConstraint EquivalentTo(IEnumerable expected) {
      return global::NUnit.Framework.Is.EquivalentTo(expected);
    }

    public static GreaterThanConstraint GreaterThan(object expected) {
      return global::NUnit.Framework.Is.GreaterThan(expected);
    }

    public static GreaterThanOrEqualConstraint GreaterThanOrEqualTo(object expected) {
      return global::NUnit.Framework.Is.GreaterThanOrEqualTo(expected);
    }

    public static RangeConstraint InRange(IComparable from, IComparable to) {
      return global::NUnit.Framework.Is.InRange(from, to);
    }

    public static InstanceOfTypeConstraint InstanceOf(Type expectedType) {
      return global::NUnit.Framework.Is.InstanceOf(expectedType);
    }

    public static InstanceOfTypeConstraint InstanceOf<TExpected>() {
      return global::NUnit.Framework.Is.InstanceOf<TExpected>();
    }

    public static LessThanConstraint LessThan(object expected) {
      return global::NUnit.Framework.Is.LessThan(expected);
    }

    public static LessThanOrEqualConstraint LessThanOrEqualTo(object expected) {
      return global::NUnit.Framework.Is.LessThanOrEqualTo(expected);
    }

    public static SameAsConstraint SameAs(object expected) {
      return global::NUnit.Framework.Is.SameAs(expected);
    }

    public static SamePathConstraint SamePath(string expected) {
      return global::NUnit.Framework.Is.SamePath(expected);
    }

    public static SamePathOrUnderConstraint SamePathOrUnder(string expected) {
      return global::NUnit.Framework.Is.SamePathOrUnder(expected);
    }

    public static SubPathConstraint SubPathOf(string expected) {
      return global::NUnit.Framework.Is.SubPathOf(expected);
    }

    public static CollectionSubsetConstraint SubsetOf(IEnumerable expected) {
      return global::NUnit.Framework.Is.SubsetOf(expected);
    }

    public static CollectionSupersetConstraint SupersetOf(IEnumerable expected) {
      return global::NUnit.Framework.Is.SupersetOf(expected);
    }

    public static ExactTypeConstraint TypeOf(Type expectedType) {
      return global::NUnit.Framework.Is.TypeOf(expectedType);
    }

    public static ExactTypeConstraint TypeOf<TExpected>() {
      return global::NUnit.Framework.Is.TypeOf<TExpected>();
    }
  }
}