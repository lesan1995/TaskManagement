using Microsoft.Extensions.Logging;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.User;
using TaskManagement.SharedKernel;
using TaskManagement.SharedKernel.Results;

namespace TaskManagement.Core.Services
{
    public class SendNotificationService(IRepository<Notification> _repository,
        ILogger<SendNotificationService> _logger) : ISendNotificationService
    {
        public async Task<Result> SendNotification(IEnumerable<UserId> userIds, NotificationTitle title, NotificationContent content)
        {
            _logger.LogInformation($"Creating notification: {title.Value}");
            var newNotification = Notification.Create(title, content).Send(userIds);
            await _repository.AddAsync(newNotification);
            return Result.Success();
        }
    }
}
