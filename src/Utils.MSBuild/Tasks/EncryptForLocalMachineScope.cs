using System;
using DavidLievrouw.Utils.Crypto;
using DavidLievrouw.Utils.MSBuild.Tasks.Handlers;
using DavidLievrouw.Utils.MSBuild.Tasks.Handlers.Models;
using DavidLievrouw.Utils.MSBuild.Tasks.Handlers.Models.Validation;
using Microsoft.Build.Framework;

namespace DavidLievrouw.Utils.MSBuild.Tasks {
  public class EncryptForLocalMachineScope : CustomTask {
    IHandler<EncryptForLocalMachineScopeRequest, string> _encryptForLocalMachineScopeQueryHandler;
    static readonly object Lock = new object();

    public override bool Execute() {
      Logger.LogMessage(MessageImportance.High, "Encrypting: " + (StringToEncrypt ?? "[NULL]"));
      EncryptedString = EncryptForLocalMachineScopeQueryHandler.Handle(
        new EncryptForLocalMachineScopeRequest {
          StringToEncrypt = StringToEncrypt,
          Purposes = Purposes
        }).Result;
      Logger.LogMessage(MessageImportance.High, "Encrypted successfully.");

      return true;
    }

    [Output]
    public string EncryptedString { get; set; }

    [Required]
    public string StringToEncrypt { get; set; }

    public string[] Purposes { get; set; }

    /// <remarks>
    ///   Property injection, with a local default, is needed, because MSBuild requires a default constructor.
    /// </remarks>
    public IHandler<EncryptForLocalMachineScopeRequest, string> EncryptForLocalMachineScopeQueryHandler {
      get {
        lock (Lock) {
          return _encryptForLocalMachineScopeQueryHandler ??
                 (_encryptForLocalMachineScopeQueryHandler = new ValidationAwareHandler<EncryptForLocalMachineScopeRequest, string>(
                   new EncryptForLocalMachineScopeRequestValidator(),
                   new EncryptForLocalMachineScopeQueryHandler(
                     new LocalMachineScopeStringEncryptor(new EntropyCreator(), new DataProtectorFactory()))));
        }
      }
      internal set {
        lock (Lock) {
          if (value == null) throw new ArgumentNullException("value");
          _encryptForLocalMachineScopeQueryHandler = value;
        }
      }
    }
  }
}