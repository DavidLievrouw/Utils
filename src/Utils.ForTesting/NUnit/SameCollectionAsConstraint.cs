using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace DavidLievrouw.Utils.ForTesting.NUnit {
  public class SameCollectionAsConstraint<T> : Constraint {
    readonly IEnumerable<string> _membersToIgnore;
    readonly IEnumerable<T> _expected;

    public SameCollectionAsConstraint(IEnumerable<T> expected) : this(expected, null) {}
    public SameCollectionAsConstraint(IEnumerable<T> expected, IEnumerable<string> membersToIgnore) {
      _expected = expected;
      _membersToIgnore = membersToIgnore;
    }

    public override ConstraintResult ApplyTo<TActual>(TActual actual) {
      var typedActual = actual as IEnumerable<T>;
      var sameCollectionAs = SameCollectionAs(typedActual);
      return new ConstraintResult(this, actual, sameCollectionAs);
    }

    bool SameCollectionAs(IEnumerable<T> actual) {
      if (actual == null && _expected == null) return true;
      if (actual != null && _expected == null) return false;
      if (actual == null && _expected != null) return false;
      return CompareNetObjects.ExtensionsForT.IsSameCollectionAs(actual, _expected, _membersToIgnore);
    }
  }
}