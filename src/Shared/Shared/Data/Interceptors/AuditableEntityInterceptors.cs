
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.DDD;

namespace Shared.Data.Interceptors
{
    public class AuditableEntityInterceptors : SaveChangesInterceptor
    {

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {

            var entities = context.ChangeTracker.Entries<IEntity>().ToList();

            foreach (EntityEntry<IEntity> entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "Admin";
                    entry.Entity.CreatedAt = DateTime.Now;
                }

                entry.Entity.LastModifiedBy = "Admin";
                entry.Entity.LastModifiedAt = DateTime.Now;


            }
        }
    }
    
}
