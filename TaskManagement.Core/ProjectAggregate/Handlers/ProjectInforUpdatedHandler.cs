namespace TaskManagement.Core.ProjectAggregate.Handlers
{
    public class ProjectInforUpdatedHandler(
        ILogger<ProjectInforUpdatedHandler> logger,
        ISendNotificationService sendNotificationService) : INotificationHandler<ProjectInforUpdatedEvent>
    {
        public async ValueTask Handle(ProjectInforUpdatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling Project infor updated event for {}", domainEvent.Project.Id);
            var userReceives = domainEvent.Project.Members.Select(x => x.UserId);
            await sendNotificationService.SendNotifications(
                userReceives,
                NotificationContent.Create($"Project named '{domainEvent.OldName}' has been updated information"));
        }
    }
}
