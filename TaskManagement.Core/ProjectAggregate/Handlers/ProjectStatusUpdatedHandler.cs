using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.ProjectAggregate.Events;

namespace TaskManagement.Core.ProjectAggregate.Handlers
{
    public class ProjectStatusUpdatedHandler(
        ILogger<ProjectStatusUpdatedHandler> logger,
        ISendNotificationService sendNotificationService) : INotificationHandler<ProjectStatusUpdatedEvent>
    {
        public async Task Handle(ProjectStatusUpdatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling project status updated event for {}", domainEvent.Project.Id);
            await sendNotificationService.SendNotifications(
                domainEvent.Project.Members.Select(x => x.UserId),
                NotificationContent.Create($"Project {domainEvent.Project.Name} has been updated to {domainEvent.Project.Status}")
                );
        }
    }
}
