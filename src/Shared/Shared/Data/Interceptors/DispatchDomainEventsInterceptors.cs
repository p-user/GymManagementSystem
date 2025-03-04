using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.DDD;

namespace Shared.Data.Interceptors
{
    public class DispatchDomainEventsInterceptors(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override  async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        }

        private async Task DispatchDomainEvents(DbContext dbContext)
        {
            if (dbContext == null) { return; }


            //retrive entitties with domain events
            var aggregates = dbContext.ChangeTracker
                .Entries<IAggregation>()
                .Where(s => s.Entity.Events.Any())
                .Select(s => s.Entity);

            //retrive all domainEvents

            var domainEvents = aggregates.SelectMany(s => s.Events).ToList();
            foreach (var item in aggregates)
            {
                item.ClearEvents();
            }

            //dispatch events
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
