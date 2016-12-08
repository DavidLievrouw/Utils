using Microsoft.Build.Framework;

namespace DavidLievrouw.Utils.MSBuild.Tasks {
  public interface ITaskLogger {
    void LogMessage(MessageImportance importance, string message, params object[] messageArgs);
  }
}