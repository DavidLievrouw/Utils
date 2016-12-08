using System.Collections.Generic;

namespace DavidLievrouw.Utils.MSBuild.Tasks.Handlers.Models {
  public class EncryptForLocalMachineScopeRequest {
    public string StringToEncrypt { get; set; }
    public IEnumerable<string> Purposes { get; set; }
  }
}