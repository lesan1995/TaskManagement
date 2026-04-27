using TaskManagement.Core.Interfaces;
using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.ProjectAggregate.Events;

namespace TaskManagement.Core.ProjectAggregate.Handlers
{
    public class ProjectIssueResolvedHandler(
        ILogger<ProjectIssueResolvedHandler> logger,
        ISendNotificationService sendNotificationService) : INotificationHandler<ProjectIssueResolvedEvent>
    {
        public async Task Handle(ProjectIssueResolvedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Handling project issue resolved event for {domainEvent.Project.Name}");
            await sendNotificationService.SendNotifications(
                domainEvent.Project.Members.Where(x => x.IsMemberShip()).Select(x => x.UserId),
                NotificationContent.Create($"Issue {domainEvent.IssueId} just had resolved")
                );
        }
    }
}
