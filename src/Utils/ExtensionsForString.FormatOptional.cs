using System;
using System.Collections.Generic;

namespace DavidLievrouw.Utils {
  public static partial class ExtensionsForString {
    public static string FormatOptional(this string stringToFormat, params string[] args) {
      if (stringToFormat == null) return null;
      if (args == null) throw new ArgumentNullException(nameof(args));

      var numReplacements = 0;
      while (stringToFormat.Contains("{" + numReplacements + "}")) numReplacements++;

      var parts = new List<string>(args);
      for (var c = parts.Count; c < numReplacements; ++c) parts.Add(string.Empty);
      return string.Format(stringToFormat, parts.ToArray());
    }
  }
}