using MediatR;

namespace TaskManagement.SharedKernel
{
    public interface IDomainEvent : INotification
    {
        DateTime DateOccurred { get; }
    }
}
