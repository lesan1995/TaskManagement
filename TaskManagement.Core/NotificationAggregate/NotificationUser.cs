using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.NotificationAggregate
{
    public class NotificationUser(NotificationId notificationId, UserId userId) : EntityBaseWithoutId
    {
        public NotificationId NotificationId { get; private set; } = notificationId;
        public UserId UserId { get; private set; } = userId;
        public bool IsRead { get; private set; } = false;
        public void Read() => IsRead = true;
    }
}
