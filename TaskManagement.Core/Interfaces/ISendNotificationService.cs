using TaskManagement.SharedKernel.Results;

namespace TaskManagement.Core.Interfaces
{
    public interface ISendNotificationService
    {
        public Task<Result> SendNotification(UserId userId, NotificationContent content);
        public Task<Result> SendNotifications(IEnumerable<UserId> userIds, NotificationContent content);
    }
}
