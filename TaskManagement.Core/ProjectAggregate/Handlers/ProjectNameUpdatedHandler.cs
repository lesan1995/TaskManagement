using MediatR;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.ProjectAggregate.Events;

namespace TaskManagement.Core.ProjectAggregate.Handlers
{
    public class ProjectNameUpdatedHandler(
        ILogger<ProjectNameUpdatedHandler> logger,
        ISendNotificationService sendNotificationService) : INotificationHandler<ProjectNameUpdatedEvent>
    {
        public async Task Handle(ProjectNameUpdatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling Project name updated event for {}", domainEvent.Project.Id);
            var userReceives = domainEvent.Project.Members.Select(x => x.UserId);
            await sendNotificationService.SendNotification(
                userReceives,
                NotificationTitle.Create($"Project update to new name"),
                NotificationContent.Create($"Project named '{domainEvent.OldName}' has been updated to new name"));
        }
    }
}
