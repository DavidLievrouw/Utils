using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace DavidLievrouw.Utils.ForTesting.NUnit {
  public class SamePropertyValuesConstraint : Constraint {
    readonly object _expected;
    readonly bool _ignoreCollectionOrder;
    readonly IEnumerable<string> _membersToIgnore;

    public SamePropertyValuesConstraint(object expected) : this(expected, false, null) {}
    public SamePropertyValuesConstraint(object expected, IEnumerable<string> membersToIgnore) : this(expected, false, membersToIgnore) { }
    public SamePropertyValuesConstraint(object expected, bool ignoreCollectionOrder) : this(expected, ignoreCollectionOrder, null) {}
    public SamePropertyValuesConstraint(object expected, bool ignoreCollectionOrder, IEnumerable<string> membersToIgnore) {
      _expected = expected;
      _ignoreCollectionOrder = ignoreCollectionOrder;
      _membersToIgnore = membersToIgnore;
    }

    public override ConstraintResult ApplyTo<TActual>(TActual actual) {
      var hasSamePropertyValues = HasSamePropertyValuesAs(actual);
      return new ConstraintResult(this, actual, hasSamePropertyValues);
    }

    bool HasSamePropertyValuesAs(object actual) {
      if (actual == null && _expected == null) return true;
      if (actual != null && _expected == null) return false;
      if (actual == null && _expected != null) return false;
      return CompareNetObjects.ExtensionsForT.HasSamePropertyValuesAs(actual, _expected, _ignoreCollectionOrder, _membersToIgnore);
    }
  }
}