using TaskManagement.Core.Interfaces;
using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.ProjectAggregate.Events;

namespace TaskManagement.Core.ProjectAggregate.Handlers
{
    public class ProjectMemberRemovedHandler(
        ILogger<ProjectMemberRemovedHandler> logger,
        ISendNotificationService sendNotificationService) : INotificationHandler<ProjectMemberRemovedEvent>
    {
        public async Task Handle(ProjectMemberRemovedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Handling project member removed event for {domainEvent.Project.Id}");
            await sendNotificationService.SendNotifications(
                domainEvent.Project.Members.Where(x => x.IsMemberShip()).Select(x => x.UserId),
                NotificationContent.Create($"User {domainEvent.UserRemovedId} just out of project {domainEvent.Project.Name}"));
        }
    }
}
