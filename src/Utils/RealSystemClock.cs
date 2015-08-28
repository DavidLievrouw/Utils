using System;

namespace DavidLievrouw.Utils {
  public class RealSystemClock : ISystemClock {
    public DateTimeOffset Now {
      get { return DateTimeOffset.Now; }
    }
  }
}