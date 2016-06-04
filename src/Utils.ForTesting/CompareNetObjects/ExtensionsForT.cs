using System.Collections.Generic;
using System.Linq;
using KellermanSoftware.CompareNetObjects;

namespace DavidLievrouw.Utils.ForTesting.CompareNetObjects {
  public static class ExtensionsForT {
    public static bool HasSamePropertyValuesAs<T>(this T first, T second) {
      return HasSamePropertyValuesAs(first, second, false, null);
    }

    public static bool HasSamePropertyValuesAs<T>(this T first, T second, bool ignoreCollectionOrder) {
      return HasSamePropertyValuesAs(first, second, ignoreCollectionOrder, null);
    }

    public static bool HasSamePropertyValuesAs<T>(this T first, T second, IEnumerable<string> membersToIgnore) {
      return HasSamePropertyValuesAs(first, second, false, membersToIgnore);
    }

    public static bool HasSamePropertyValuesAs<T>(this T first, T second, bool ignoreCollectionOrder, IEnumerable<string> membersToIgnore) {
      var comparison = first.CompareTo(second, ignoreCollectionOrder, membersToIgnore);
      return comparison.AreEqual;
    }

    public static ComparisonResult CompareTo<T>(this T first, T second) {
      return CompareTo(first, second, false, null);
    }

    public static ComparisonResult CompareTo<T>(this T first, T second, bool ignoreCollectionOrder) {
      return CompareTo(first, second, ignoreCollectionOrder, null);
    }

    public static ComparisonResult CompareTo<T>(this T first, T second, IEnumerable<string> membersToIgnore) {
      return CompareTo(first, second, false, membersToIgnore);
    }

    public static ComparisonResult CompareTo<T>(this T first, T second, bool ignoreCollectionOrder, IEnumerable<string> membersToIgnore) {
      var compareLogic = new CompareLogic {
        Config = new ComparisonConfig {
          IgnoreObjectTypes = true, // allows anonymous types to be compared
          IgnoreCollectionOrder = ignoreCollectionOrder,
          MembersToIgnore = membersToIgnore?.ToList() ?? new List<string>()
        }
      };
      return compareLogic.Compare(first, second);
    }

    public static bool IsSameCollectionAs<T>(this IEnumerable<T> first, IEnumerable<T> second) {
      return IsSameCollectionAs(first, second, null);
    }

    public static bool IsSameCollectionAs<T>(this IEnumerable<T> first, IEnumerable<T> second, IEnumerable<string> membersToIgnore) {
      if (first == null && second == null) return true;
      if (first == null || second == null) return false;

      var materializedFirst = first.ToList();
      var materializedSecond = second.ToList();
      var compareLogic = new CompareLogic {
        Config = new ComparisonConfig {
          IgnoreObjectTypes = true, // allows anonymous types to be compared
          IgnoreCollectionOrder = true,
          MembersToIgnore = membersToIgnore?.ToList() ?? new List<string>()
        }
      };

      return
        materializedFirst.Count == materializedSecond.Count &&
        materializedFirst.All(
          itemFromFirst =>
            materializedSecond.Any(itemFromSecond => compareLogic.Compare(itemFromFirst, itemFromSecond).AreEqual));
    }

    public static IDictionary<T, IEnumerable<ComparisonResult>> CompareMany<T>(this IEnumerable<T> first, IEnumerable<T> second) {
      return CompareMany(first, second, null);
    }

    public static IDictionary<T, IEnumerable<ComparisonResult>> CompareMany<T>(this IEnumerable<T> first, IEnumerable<T> second, IEnumerable<string> membersToIgnore) {
      if (first == null && second == null) return new Dictionary<T, IEnumerable<ComparisonResult>>();
      if (first == null || second == null) return new Dictionary<T, IEnumerable<ComparisonResult>>();

      var materializedFirst = first.ToList();
      var materializedSecond = second.ToList();
      var compareLogic = new CompareLogic {
        Config = new ComparisonConfig {
          IgnoreObjectTypes = true, // allows anonymous types to be compared
          IgnoreCollectionOrder = true,
          MembersToIgnore = membersToIgnore?.ToList() ?? new List<string>()
        }
      };

      var keyValuePairs = materializedFirst.Select(itemFromFirst => new KeyValuePair<T, IEnumerable<ComparisonResult>>(
        itemFromFirst, materializedSecond.Select(itemFromSecond =>  compareLogic.Compare(itemFromFirst, itemFromSecond))));

      return keyValuePairs.ToDictionary(x => x.Key, x => x.Value);
    }
  }
}