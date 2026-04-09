using MediatR;

namespace TaskManagement.SharedKernel.Common
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
