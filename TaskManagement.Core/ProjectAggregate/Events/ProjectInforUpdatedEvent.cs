namespace TaskManagement.Core.ProjectAggregate.Events
{
    public sealed class ProjectInforUpdatedEvent(Project project, ProjectName oldName) : DomainEventBase
    {
        public Project Project { get; init; } = project;
        public ProjectName OldName { get; init; } = oldName;
    }
}
