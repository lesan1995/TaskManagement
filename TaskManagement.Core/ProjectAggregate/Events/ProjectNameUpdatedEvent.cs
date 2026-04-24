using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate.Events
{
    public sealed class ProjectNameUpdatedEvent(Project project, ProjectName oldName) : DomainEventBase
    {
        public Project Project { get; init; } = project;
        public ProjectName OldName { get; init; } = oldName;
    }
}
