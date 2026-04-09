using MediatR;

namespace TaskManagement.SharedKernel.Common
{
    public interface IDomainEvent : INotification
    {
        DateTime DateOccurred { get; }
    }
}
