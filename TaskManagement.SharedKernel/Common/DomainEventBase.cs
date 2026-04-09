namespace TaskManagement.SharedKernel.Common
{
    public abstract class DomainEventBase : IDomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
