using System.Threading.Tasks;

namespace DavidLievrouw.Utils {
  public interface IQueryHandler<TResponse> {
    Task<TResponse> Handle();
  }

  public interface IQueryHandler<in TRequest, TResponse> {
    Task<TResponse> Handle(TRequest request);
  }
}