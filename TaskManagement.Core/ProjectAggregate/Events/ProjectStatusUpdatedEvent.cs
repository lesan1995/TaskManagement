namespace TaskManagement.Core.ProjectAggregate.Events
{
    public sealed class ProjectStatusUpdatedEvent(Project project) : DomainEventBase
    {
        public Project Project { get; init; } = project;
    }
}
