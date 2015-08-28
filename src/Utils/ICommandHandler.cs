using System.Threading.Tasks;

namespace DavidLievrouw.Utils {
  public interface ICommandHandler<in TCommand> {
    Task Handle(TCommand command);
  }
}