using TaskManagement.Core.Interfaces;
using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.ProjectAggregate.Events;

namespace TaskManagement.Core.ProjectAggregate.Handlers
{
    public class ProjectTaskAssignedHandler(
        ILogger<ProjectTaskAssignedHandler> logger,
        ISendNotificationService sendNotificationService) : INotificationHandler<ProjectTaskAssignedEvent>
    {
        public async ValueTask Handle(ProjectTaskAssignedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling project task assigned event for {}", domainEvent.Project.Id);
            await sendNotificationService.SendNotification(
                domainEvent.AssignedId,
                NotificationContent.Create($"You have just been assigned the task {domainEvent.Task.Title} belonging to the project {domainEvent.Project.Name}")
                );
        }
    }
}
