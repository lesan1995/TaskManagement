using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate.Events
{
    public sealed class ProjectIssueAddedEvent(Project project) : DomainEventBase
    {
        public Project Project { get; init; } = project;
    }
}
