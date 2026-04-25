using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate.Events
{
    public sealed class ProjectMemberAddedEvent(Project project, UserId newMemberId) : DomainEventBase
    {
        public Project Project { get; init; } = project;
        public UserId NewMemberId { get; init; } = newMemberId;
    }
}
