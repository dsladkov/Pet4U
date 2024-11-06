using System.Data;

namespace Pet4U.Application;
public interface IUnitOfWork
{
  Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);
  Task SaveChanges(CancellationToken cancellationToken= default);
}