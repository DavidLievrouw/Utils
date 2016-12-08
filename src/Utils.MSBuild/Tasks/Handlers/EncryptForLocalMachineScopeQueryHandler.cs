using System;
using System.Threading.Tasks;
using DavidLievrouw.Utils.Crypto;
using DavidLievrouw.Utils.MSBuild.Tasks.Handlers.Models;

namespace DavidLievrouw.Utils.MSBuild.Tasks.Handlers {
  public class EncryptForLocalMachineScopeQueryHandler : IHandler<EncryptForLocalMachineScopeRequest, string> {
    readonly ILocalMachineScopeStringEncryptor _localMachineScopeStringEncryptor;

    public EncryptForLocalMachineScopeQueryHandler(ILocalMachineScopeStringEncryptor localMachineScopeStringEncryptor) {
      if (localMachineScopeStringEncryptor == null) throw new ArgumentNullException("localMachineScopeStringEncryptor");
      _localMachineScopeStringEncryptor = localMachineScopeStringEncryptor;
    }

    public Task<string> Handle(EncryptForLocalMachineScopeRequest request) {
      return Task.FromResult(_localMachineScopeStringEncryptor.Encrypt(request.StringToEncrypt, request.Purposes));
    }
  }
}