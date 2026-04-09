using MediatR;

namespace TaskManagement.SharedKernel
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
