using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DavidLievrouw.Utils.ForTesting.DotNet {
  public class NormalisedLineBreakInvariantStringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string> {
    public bool Equals(string x, string y) {
      if (x == null && y == null) return true;
      if (x != null && y == null) return false;
      return x != null && StringComparer.InvariantCulture.Equals(Regex.Replace(x, @"\r\n?|\n", "\n"), Regex.Replace(y, @"\r\n?|\n", "\n"));
    }

    public int GetHashCode(string str) {
      if (str == null) throw new ArgumentNullException(nameof(str));
      return str.GetHashCode();
    }

    public int Compare(object x, object y) {
      if (ReferenceEquals(x, y)) return 0;
      if (x == null) return -1;
      if (y == null) return 1;

      var strX = x as string;
      var strY = y as string;
      if (strX == null || strY == null) return StringComparer.InvariantCulture.Compare(x, y);

      return ((IComparer<string>)this).Compare(strX, strY);
    }

    bool IEqualityComparer.Equals(object x, object y) {
      if (ReferenceEquals(x, y)) return true;
      if (x == null || y == null) return false;

      var strX = x as string;
      var strY = y as string;
      if (strX == null) return x.Equals(y);
      if (strY == null) return false;

      return ((IEqualityComparer<string>)this).Equals(strX, strY);
    }

    public int GetHashCode(object obj) {
      if (obj == null) throw new ArgumentNullException(nameof(obj));
      return obj.GetHashCode();
    }

    public int Compare(string x, string y) {
      if (ReferenceEquals(x, y)) return 0;
      if (x == null) return -1;
      if (y == null) return 1;
      return StringComparer.InvariantCulture.Compare(Regex.Replace(x, @"\r\n?|\n", "\n"), Regex.Replace(y, @"\r\n?|\n", "\n"));
    }
  }
}