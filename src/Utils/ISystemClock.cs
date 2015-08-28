using System;

namespace DavidLievrouw.Utils {
  public interface ISystemClock {
    DateTimeOffset Now { get; }
  }
}