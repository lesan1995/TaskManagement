using TaskManagement.Core.Interfaces;
using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.ProjectAggregate.Events;

namespace TaskManagement.Core.ProjectAggregate.Handlers
{
    public class ProjectIssueAddedHandler(
        ILogger<ProjectIssueAddedHandler> logger,
        ISendNotificationService sendNotificationService) : INotificationHandler<ProjectIssueAddedEvent>
    {
        public async ValueTask Handle(ProjectIssueAddedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Handling project issue added event for {domainEvent.Project.Name}");
            await sendNotificationService.SendNotifications(
                domainEvent.Project.Members.Where(x => x.IsMemberShip).Select(x => x.UserId),
                NotificationContent.Create($"Project {domainEvent.Project.Name} just had a issue")
                );
        }
    }
}
