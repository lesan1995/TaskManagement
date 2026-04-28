using Mediator;

namespace TaskManagement.SharedKernel
{
    public interface IDomainEvent : INotification
    {
        DateTime DateOccurred { get; }
    }
}
