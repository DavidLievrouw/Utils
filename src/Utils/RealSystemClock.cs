using System;

namespace DavidLievrouw.Utils {
  public class RealSystemClock : ISystemClock {
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
  }
}