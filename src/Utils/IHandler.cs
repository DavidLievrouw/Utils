using System.Threading.Tasks;

namespace DavidLievrouw.Utils {
  public interface IHandler<TResponse> {
    Task<TResponse> Handle();
  }

  public interface IHandler<in TRequest, TResponse> {
    Task<TResponse> Handle(TRequest request);
  }
}