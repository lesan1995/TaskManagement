using TaskManagement.Core.ProjectAggregate.Events;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate
{
    public class Project(ProjectName name, string description) : AuditableEntityBase<Project, ProjectId>, IAggregateRoot
    {
        public ProjectName Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public ProjectDeadline Deadline { get; private set; } = default!;
        public ProjectStatus Status { get; private set; } = ProjectStatus.NotStarted;
        private readonly List<ProjectMember> _members = new();
        public IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();
        public readonly List<TaskItem> _tasks = new();
        public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
        public Project UpdateName(ProjectName name)
        {
            if(Name == name) return this;
            Name = name;
            RegisterDomainEvents(new ProjectNameUpdatedEvent(this, oldName: name));
            return this;
        }
        public Project UpdateDescription(string description)
        {
            if (Description == description) return this;
            Description = description;
            return this;
        }
        public Project UpdateStatus(ProjectStatus status)
        {
            Status = status;
            return this;
        }
        public Project SetDeadline(ProjectDeadline deadline)
        {
            Deadline = deadline;
            return this;
        }
        public Project AddMember(IEnumerable<ProjectMember> members)
        {
            _members.AddRange(members);
            return this;
        }
        public Project RemoveMember(ProjectMember member)
        {
            _members.Remove(member);
            return this;
        }
        public Project AddTask(TaskItem task)
        {
            _tasks.Add(task);
            return this;
        }
        public Project RemoveTask(TaskItem task)
        {
            _tasks.Remove(task);
            return this;
        }
    }
}
