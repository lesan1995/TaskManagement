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
        public int Progress => (_tasks.Any() ? _tasks.Count(x => x.IsDone) / _tasks.Count() : 0) * 100;
        private readonly List<Issue> _issues = new();
        public IReadOnlyCollection<Issue> Issues => _issues.AsReadOnly();
        private Project(ProjectName name, string description)
        {
            Name = name;
            Description = description;
            Deadline = ProjectDeadline.Default();
            Status = ProjectStatus.NotStarted;
        }
        public static Project Create(ProjectName name, string description) => new Project(name, description);
        private void EnsureProjectActive()
        {
            if (IsDeleted)
                throw new InvalidOperationException("Project has been soft deleted");
            if (Status == ProjectStatus.Cancelled)
                throw new InvalidOperationException("Project has been cancelled");
        }
        public Project UpdateInfor(ProjectName name, string description)
        {
            EnsureProjectActive();
            if (Name == name && Description == description) return this;
            var oldName = Name;
            Name = name;
            Description = description;
            RegisterDomainEvents(new ProjectInforUpdatedEvent(this, oldName));
            return this;
        }
        public void SetDeadline(ProjectDeadline deadline)
        {
            EnsureProjectActive();
            Deadline = deadline;
        }
        public void Cancel() => Status = ProjectStatus.Cancelled;
        public void Hold() => Status = ProjectStatus.OnHold;

        //-----------------Member-----------
        //----------------------------------
        private ProjectMember FindMember(UserId userId) =>
            _members.FirstOrDefault(x => x.UserId.Equals(userId)) ?? throw new InvalidOperationException("Member does not exists");
        public Project AddMember(UserId userId, ProjectMemberRole role)
        {
            EnsureProjectActive();
            if (_members.Any(x => x.UserId.Equals(userId)))
                throw new InvalidOperationException("Member already exists");
            _members.Add(ProjectMember.Create(Id, userId, role));
            RegisterDomainEvents(new ProjectMemberAddedEvent(this, userId));
            return this;
        }
        public Project RemoveMember(UserId userId)
        {
            EnsureProjectActive();
            _members.Remove(FindMember(userId));
            RegisterDomainEvents(new ProjectMemberRemovedEvent(this, userId));
            return this;
        }

        //-----------------Task-----------
        //----------------------------------
        private TaskItem FindTask(TaskItemId taskItemId) =>
            _tasks.FirstOrDefault(x => x.Id.Equals(taskItemId)) ?? throw new InvalidOperationException("Task does not exists");
        private void UpdateProjectStatus(int previousProgress)
        {
            if (previousProgress == Progress) return;
            if (Progress == 0) Status = ProjectStatus.NotStarted;
            else if (Progress > 0 && Progress < 100) Status = ProjectStatus.InProgress;
            else Status = ProjectStatus.Completed;
            RegisterDomainEvents(new ProjectStatusUpdatedEvent(this));
        }
        public void AddTask(TaskItemTitle title, string description)
        {
            EnsureProjectActive();
            var oldProgress = Progress;
            _tasks.Add(TaskItem.Create(Id, title, description, TaskItemIndex.Create(_tasks.Count())));
            UpdateProjectStatus(oldProgress);
        }
        public void UpdateTask(TaskItemId taskItemId, TaskItemTitle title, string description)
        {
            EnsureProjectActive();
            FindTask(taskItemId).UpdateInfor(title, description);
        }
        public void ReorderTasks(List<TaskItemId> newOrders)
        {
            EnsureProjectActive();
            var currentOrders = _tasks.Select(x => x.Id).ToList();
            if (newOrders.Distinct().Count() != currentOrders.Count()
                || !newOrders.All(currentOrders.Contains)
                || !currentOrders.All(newOrders.Contains))
                throw new InvalidOperationException("New order list must contain exactly the same tasks as current list.");
            for (int newOverIndex = 0; newOverIndex < newOrders.Count(); newOverIndex++)
                FindTask(newOrders[newOverIndex]).UpdateOverIndex(TaskItemIndex.Create(newOverIndex));
        }
        public void MarkDoneTask(TaskItemId taskItemId, bool isDone)
        {
            EnsureProjectActive();
            var oldProgress = Progress;
            FindTask(taskItemId).MarkDone(isDone);
            UpdateProjectStatus(oldProgress);
        }
        public void AssignTask(TaskItemId taskItemId, UserId userId)
        {
            EnsureProjectActive();
            FindMember(userId);
            var task = FindTask(taskItemId);
            task.Assign(userId);
            RegisterDomainEvents(new ProjectTaskAssignedEvent(this, task, userId));
        }
        public void UnAssignTask(TaskItemId taskItemId, UserId userId)
        {
            EnsureProjectActive();
            FindMember(userId);
            var task = FindTask(taskItemId);
            if (task.AssigneeId == default || task.AssigneeId != userId)
                throw new InvalidOperationException("User does not belong to this task");
            task.UnAssign();
        }
        public void RemoveTask(TaskItemId taskItemId)
        {
            EnsureProjectActive();
            _tasks.Remove(FindTask(taskItemId));
        }

        //-----------------Issue-----------
        //----------------------------------
        private Issue FindIssue(IssueId issueId) =>
            _issues.FirstOrDefault(x => x.Id.Equals(issueId)) ?? throw new InvalidOperationException("Issue does not exists");
        public void AddIssue(IssueContent content, IssueSeverity severity)
        {
            EnsureProjectActive();
            _issues.Add(Issue.Create(Id, content, severity));
        }
        public void UpdateIssue(IssueId issueId, IssueContent content, IssueSeverity severity)
        {
            EnsureProjectActive();
            FindIssue(issueId).UpdateInfor(content, severity);
        }
        public void ResolveIssue(IssueId issueId, IssueResolvedComment comment)
        {
            EnsureProjectActive();
            FindIssue(issueId).Resolve(comment);
        }
        public void RemoveIssue(IssueId issueId)
        {
            EnsureProjectActive();
            _issues.Remove(FindIssue(issueId));
        }
    }
}
