using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.SharedKernel.Common
{
    public abstract class HasDomainEventsBase : IHasDomainEvents
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void RegisterDomainEvents(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
