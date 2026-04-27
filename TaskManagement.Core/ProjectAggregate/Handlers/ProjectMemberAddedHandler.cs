using TaskManagement.Core.Interfaces;
using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.ProjectAggregate.Events;

namespace TaskManagement.Core.ProjectAggregate.Handlers
{
    public class ProjectMemberAddedHandler(
        ILogger<ProjectMemberAddedHandler> logger,
        ISendNotificationService sendNotificationService) : INotificationHandler<ProjectMemberAddedEvent>
    {
        public async Task Handle(ProjectMemberAddedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Handling project member added event for {domainEvent.Project.Name}");
            await sendNotificationService.SendNotifications(
                domainEvent.Project.Members.Where(x => x.IsMemberShip()).Select(x => x.UserId),
                NotificationContent.Create($"User {domainEvent.NewMemberId} just joined project {domainEvent.Project.Name}")
                );
        }
    }
}
