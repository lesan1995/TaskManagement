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
        public ProjectDeadline Deadline { get; private set; } = ProjectDeadline.Default();
        public ProjectStatus Status { get; private set; } = ProjectStatus.NotStarted;
        private readonly List<ProjectMember> _members = new();
        public IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();
        private readonly List<TaskItem> _tasks = new();
        public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
        public int Progress => _tasks.Any() ? _tasks.Count(x => x.IsDone) / _tasks.Count() : 0;
        private readonly List<Issue> _issues = new();
        public IReadOnlyCollection<Issue> Issues => _issues.AsReadOnly();
        private Project(ProjectName name, string description)
        {
            Name = name;
            Description = description;
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
            if(Status == status) return this;
            Status = status;
            RegisterDomainEvents(new ProjectStatusUpdatedEvent(this));
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
            RegisterDomainEvents(new ProjectMemberAddedEvent(this, userId));
            return this;
        }
        public Project RemoveMember(UserId userId)
        {
            var member = _members.FirstOrDefault(x => x.UserId.Equals(userId));
            if (member == null)
                throw new InvalidOperationException("Member does not exists");
            _members.Remove(member);
            RegisterDomainEvents(new ProjectMemberRemovedEvent(this, userId));
            return this;
        }
        public void AddTask(TaskItemTitle title, string description)
        {
            var taskWithLastOverIndex = _tasks.OrderByDescending(x => x.OverIndex).FirstOrDefault();
            var taskOverIndex = taskWithLastOverIndex?.OverIndex + 1 ?? 0;
            _tasks.Add(TaskItem.Create(Id, title, description, TaskItemIndex.Create(taskOverIndex)));
        }
        public void UpdateTask(TaskItemId taskItemId, TaskItemTitle title, string description)
        {
            var task = _tasks.FirstOrDefault(x => x.Id.Equals(taskItemId));
            if (task == null)
                throw new InvalidOperationException("Member does not exists");

            task.UpdateInfor(title, description);
        }
        public void MarkDoneTask(TaskItemId taskItemId)
        {
            var task = _tasks.FirstOrDefault(x => x.Id.Equals(taskItemId));
            if (task == null)
                throw new InvalidOperationException("Task does not exists");

            task.MarkDone();
        }
        public void UnDoneTask(TaskItemId taskItemId)
        {
            var task = _tasks.FirstOrDefault(x => x.Id.Equals(taskItemId));
            if (task == null)
                throw new InvalidOperationException("Member does not exists");

            task.UnDone();
        }
        public void AssignTask(TaskItemId taskItemId, UserId userId)
        {
            var task = _tasks.FirstOrDefault(x => x.Id.Equals(taskItemId));
            if (task == null)
                throw new InvalidOperationException("Task does not exists");

            var member = _members.FirstOrDefault(x => x.UserId.Equals(userId));
            if (member == null)
                throw new InvalidOperationException("Member does not exists");

            task.Assign(userId);
            RegisterDomainEvents(new ProjectTaskAssignedEvent(this, task, userId));
        }
        public void UnAssignTask(TaskItemId taskItemId, UserId userId)
        {
            var task = _tasks.FirstOrDefault(x => x.Id.Equals(taskItemId));
            if (task == null)
                throw new InvalidOperationException("Task does not exists");

            var member = _members.FirstOrDefault(x => x.UserId.Equals(userId));
            if (member == null)
                throw new InvalidOperationException("Member does not exists");

            if (task.AssigneeId == default || task.AssigneeId != userId)
                throw new InvalidOperationException("User does not belong to this task");

            task.UnAssign();
        }
        public void RemoveTask(TaskItemId taskItemId)
        {
            var task = _tasks.FirstOrDefault(x => x.Id.Equals(taskItemId));
            if (task == null)
                throw new InvalidOperationException("Task does not exists");
            _tasks.Remove(task);
        }
        public void AddIssue(IssueContent content, IssueSeverity severity)
        {
            _issues.Add(Issue.Create(Id, content, severity));
        }
        public Project UpdateIssue(IssueId issueId, IssueContent content, IssueSeverity severity)
        {
            var issue = _issues.FirstOrDefault(x => x.Id.Equals(issueId));
            if (issue == null)
                throw new InvalidOperationException("Issue does not exists");
            issue.UpdateInfor(content, severity);
            return this;
        }
        public Project ResolveIssue(IssueId issueId, string comment)
        {
            var issue = _issues.FirstOrDefault(x => x.Id.Equals(issueId));
            if (issue == null)
                throw new InvalidOperationException("Issue does not exists");
            issue.Resolve(comment);
            return this;
        }
        public Project RemoveIssue(IssueId issueId)
        {
            var issue = _issues.FirstOrDefault(x => x.Id.Equals(issueId));
            if (issue == null)
                throw new InvalidOperationException("Issue does not exists");
            _issues.Remove(issue);
            return this;
        }
    }
}
