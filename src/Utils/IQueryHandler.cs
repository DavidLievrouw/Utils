using System.Threading.Tasks;

namespace DavidLievrouw.Utils {
  public interface IQueryHandler<TResult> {
    Task<TResult> Handle();
  }

  public interface IQueryHandler<in TArg, TResult> {
    Task<TResult> Handle(TArg request);
  }
}