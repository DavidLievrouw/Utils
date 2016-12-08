using FluentValidation;

namespace DavidLievrouw.Utils.MSBuild.Tasks.Handlers.Models.Validation {
  public class EncryptForLocalMachineScopeRequestValidator
    : NullAllowableValidator<EncryptForLocalMachineScopeRequest>,
      IEncryptForLocalMachineScopeRequestValidator {
    public EncryptForLocalMachineScopeRequestValidator() {
      RuleFor(request => request.StringToEncrypt)
        .NotNull()
        .WithMessage("No valid string to encrypt was specified.")
        .Must(str => !string.IsNullOrEmpty(str))
        .WithMessage("No valid string to encrypt was specified.");
    }
  }
}