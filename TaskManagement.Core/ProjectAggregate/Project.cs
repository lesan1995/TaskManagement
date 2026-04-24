using System.Data;
using TaskManagement.Core.ProjectAggregate.Events;
using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate
{
    public class Project : AuditableEntityBase<Project, ProjectId>, IAggregateRoot
    {
        public ProjectName Name { get; private set; }
        public string Description { get; private set; }
        public ProjectDeadline Deadline { get; private set; }
        public ProjectStatus Status { get; private set; }
        private readonly List<ProjectMember> _members = new();
        public IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();
        private readonly List<TaskItem> _tasks = new();
        public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
        private Project(ProjectName name, string description)
        {
            Name = name;
            Description = description;
            Deadline = default!;
            Status = ProjectStatus.NotStarted;
        }
        public static Project Create(ProjectName name, string description) => new Project(name, description);
        public Project UpdateName(ProjectName name)
        {
            if(Name == name) return this;
            var oldName = Name;
            Name = name;
            RegisterDomainEvents(new ProjectNameUpdatedEvent(this, oldName));
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
        public Project AddMember(UserId userId, ProjectMemberRole role)
        {
            if (_members.Any(x => x.UserId.Equals(userId)))
                throw new InvalidOperationException("Member already exists");
            _members.Add(ProjectMember.Create(Id, userId, role));
            return this;
        }
        public Project RemoveMember(UserId userId)
        {
            var member = _members.FirstOrDefault(x => x.UserId.Equals(userId));
            if (member == null)
                throw new InvalidOperationException("Member does not exists");
            _members.Remove(member);
            return this;
        }
        public Project AddTask(string title, string description)
        {
            var taskWithLastOverIndex = _tasks.OrderByDescending(x => x.OverIndex).FirstOrDefault();
            var taskOverIndex = taskWithLastOverIndex?.OverIndex + 1 ?? 0;
            _tasks.Add(TaskItem.Create(Id, TaskItemTitle.Create(title), description, TaskItemIndex.Create(taskOverIndex)));
            return this;
        }
        public Project RemoveTask(TaskItemId taskItemId)
        {
            var task = _tasks.FirstOrDefault(x => x.Id.Equals(taskItemId));
            if (task == null)
                throw new InvalidOperationException("Task does not exists");
            _tasks.Remove(task);
            return this;
        }
    }
}
