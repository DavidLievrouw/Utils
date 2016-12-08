using System;
using System.Threading.Tasks;
using DavidLievrouw.Utils.Crypto;
using DavidLievrouw.Utils.MSBuild.Tasks.Handlers.Models;

namespace DavidLievrouw.Utils.MSBuild.Tasks.Handlers {
  public class DecryptForLocalMachineScopeQueryHandler : IHandler<DecryptForLocalMachineScopeRequest, string> {
    readonly ILocalMachineScopeStringEncryptor _localMachineScopeStringEncryptor;

    public DecryptForLocalMachineScopeQueryHandler(ILocalMachineScopeStringEncryptor localMachineScopeStringEncryptor) {
      if (localMachineScopeStringEncryptor == null) throw new ArgumentNullException("localMachineScopeStringEncryptor");
      _localMachineScopeStringEncryptor = localMachineScopeStringEncryptor;
    }

    public Task<string> Handle(DecryptForLocalMachineScopeRequest request) {
      return Task.FromResult(_localMachineScopeStringEncryptor.Decrypt(request.StringToDecrypt, request.Purposes));
    }
  }
}