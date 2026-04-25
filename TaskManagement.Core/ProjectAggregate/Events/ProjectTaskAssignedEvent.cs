using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate.Events
{
    public sealed class ProjectTaskAssignedEvent(Project project, TaskItem task, UserId assignedId) : DomainEventBase
    {
        public Project Project { get; init; } = project;
        public TaskItem Task { get; init; } = task;
        public UserId AssignedId { get; init; } = assignedId;
    }
}
