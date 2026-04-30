using TaskManagement.SharedKernel.Results;

namespace TaskManagement.Core.Services
{
    public class SendNotificationService(IRepository<Notification> _repository,
        ILogger<SendNotificationService> _logger) : ISendNotificationService
    {
        public async Task<Result> SendNotification(UserId userId, NotificationContent content)
            => await SendNotifications([userId], content);

        public async Task<Result> SendNotifications(IEnumerable<UserId> userIds, NotificationContent content)
        {
            _logger.LogInformation($"Creating notification: {content.Summary()}");
            var newNotification = Notification.Create(content).Send(userIds);
            await _repository.AddAsync(newNotification);
            return Result.Success();
        }
    }
}
