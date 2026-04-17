using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate
{
    public class Project(ProjectName name) : AuditableEntityBase<Project, ProjectId>, IAggregateRoot
    {
        public ProjectName Name { get; private set; } = name;
        public string Description { get; private set; } = default!;
        public ProjectDeadline Deadline { get; private set; } = default!;
        public ProjectStatus Status { get; private set; } = ProjectStatus.NotStarted;
        private readonly List<ProjectMember> _members = new();
        public IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();
    }
}
