namespace TaskManagement.Core.ProjectAggregate.Events
{
    public sealed class ProjectMemberRemovedEvent(Project project, UserId userRemovedId) : DomainEventBase
    {
        public Project Project { get; init; } = project;
        public UserId UserRemovedId { get; init; } = userRemovedId;
    }
}
