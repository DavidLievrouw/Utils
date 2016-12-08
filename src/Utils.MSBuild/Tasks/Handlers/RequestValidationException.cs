using System;
using FluentValidation.Results;

namespace DavidLievrouw.Utils.MSBuild.Tasks.Handlers {
  public class RequestValidationException : Exception {
    const string DefaultMessage = "An error occurred while validating the request.";

    public RequestValidationException() : base(DefaultMessage) { }

    public RequestValidationException(string message) : base(message) { }

    public RequestValidationException(string message, Exception inner) : base(message, inner) { }

    public RequestValidationException(Exception inner) : base(DefaultMessage, inner) { }

    public RequestValidationException(ValidationResult validationResult) : base(DefaultMessage) {
      ValidationResult = validationResult;
    }

    public RequestValidationException(ValidationResult validationResult, string message) : base(message) {
      ValidationResult = validationResult;
    }

    public RequestValidationException(ValidationResult validationResult, string message, Exception inner) : base(message, inner) {
      ValidationResult = validationResult;
    }

    public RequestValidationException(ValidationResult validationResult, Exception inner) : base(DefaultMessage, inner) {
      ValidationResult = validationResult;
    }

    public ValidationResult ValidationResult { get; }
  }
}
