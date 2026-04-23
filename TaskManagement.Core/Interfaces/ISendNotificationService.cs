using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.User;
using TaskManagement.SharedKernel.Results;

namespace TaskManagement.Core.Interfaces
{
    public interface ISendNotificationService
    {
        public Task<Result> SendNotification(IEnumerable<UserId> userIds, NotificationTitle title, NotificationContent content);
    }
}
