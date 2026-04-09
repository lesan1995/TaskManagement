namespace TaskManagement.SharedKernel.Common
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents);
    }
}
