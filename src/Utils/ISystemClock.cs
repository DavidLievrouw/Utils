using System;

namespace DavidLievrouw.Utils {
  public interface ISystemClock {
    DateTimeOffset UtcNow { get; }
  }
}