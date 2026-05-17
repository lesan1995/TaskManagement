using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TaskManagement.Infrastructure.Data
{
    public class EventDispatcherInterceptor(IDomainEventDispatcher domainEventDispatcher) : SaveChangesInterceptor
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = new CancellationToken())
        {
            var context = eventData.Context;
            if(context is not AppDbContext appDbContext)
            {
                return await base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            var entitiesWithEvents = appDbContext.ChangeTracker.Entries<HasDomainEventsBase>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            var resultSaveChange = await base.SavedChangesAsync(eventData, result, cancellationToken);

            await _domainEventDispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return resultSaveChange;
        }
    }
}
