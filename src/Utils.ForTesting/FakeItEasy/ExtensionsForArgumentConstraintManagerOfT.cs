﻿using System.Collections.Generic;
using DavidLievrouw.Utils.ForTesting.CompareNetObjects;
using FakeItEasy;

namespace DavidLievrouw.Utils.ForTesting.FakeItEasy {
  public static class ExtensionsForArgumentConstraintManagerOfT {
    public static T HasSamePropertyValuesAs<T>(this IArgumentConstraintManager<T> manager, object value) {
      return HasSamePropertyValuesAs(manager, value, false, null);
    }

    public static T HasSamePropertyValuesAs<T>(this IArgumentConstraintManager<T> manager, object value, IEnumerable<string> membersToIgnore) {
      return HasSamePropertyValuesAs(manager, value, false, membersToIgnore);
    }

    public static T HasSamePropertyValuesAs<T>(this IArgumentConstraintManager<T> manager, object value, bool ignoreCollectionOrder) {
      return HasSamePropertyValuesAs(manager, value, ignoreCollectionOrder, null);
    }

    public static T HasSamePropertyValuesAs<T>(this IArgumentConstraintManager<T> manager, object value, bool ignoreCollectionOrder, IEnumerable<string> membersToIgnore) {
      return manager.Matches(
        x => x.HasSamePropertyValuesAs(value, ignoreCollectionOrder, membersToIgnore),
        x => x.Write("object that matches by property values as ").WriteArgumentValue(value));
    }

    public static IEnumerable<T> IsSameCollectionAs<T>(this IArgumentConstraintManager<IEnumerable<T>> manager, IEnumerable<T> value) {
      return IsSameCollectionAs(manager, value, null);
    }

    public static IEnumerable<T> IsSameCollectionAs<T>(this IArgumentConstraintManager<IEnumerable<T>> manager, IEnumerable<T> value, IEnumerable<string> membersToIgnore) {
      return manager.Matches(
        x => x.IsSameCollectionAs(value, membersToIgnore),
        x => x.Write("object that is same collection by property values as ").WriteArgumentValue(value));
    }
  }
}