using System.Collections.Generic;

namespace DavidLievrouw.Utils.MSBuild.Tasks.Handlers.Models {
  public class DecryptForLocalMachineScopeRequest {
    public string StringToDecrypt { get; set; }
    public IEnumerable<string> Purposes { get; set; }
  }
}