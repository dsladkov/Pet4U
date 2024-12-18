using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pet4U.Domain.PetManagement.AgregateRoot;
using Pet4U.Domain.Shared;

namespace Pet4U.Infrastructure.Interceptors;


public class SoftRestoreInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
      if(eventData.Context is null)
        return await base.SavingChangesAsync(eventData, result, cancellationToken);

      var entries = eventData.Context.ChangeTracker
          .Entries()
          .Where(e => e.State == EntityState.Deleted);

          foreach(var entry in entries)
          {
            if(entry.Entity is ISoftDeletable item)
            {
              entry.State = EntityState.Modified;
              item.Restore();
            }
          }
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}