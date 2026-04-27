using TaskManagement.Core.ProjectAggregate.Events;
using TaskManagement.Core.User;

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
        public int Progress => _tasks.Count() == 0 ? 0 : (int)Math.Round(_tasks.Count(x => x.IsDone) * 100.0 / _tasks.Count());
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
        public Project UpdateInfo(ProjectName name, string description)
        {
            EnsureProjectActive();
            if (Name == name && Description == description) return this;
            var oldName = Name;
            Name = name;
            Description = description;
            RegisterDomainEvents(new ProjectInforUpdatedEvent(this, oldName));
            return this;
        }
        public Project SetDeadline(ProjectDeadline deadline)
        {
            EnsureProjectActive();
            Deadline = deadline;
            return this;
        }
        public Project Cancel()
        {
            EnsureProjectActive();
            Status = ProjectStatus.Cancelled;
            RegisterDomainEvents(new ProjectStatusUpdatedEvent(this));
            return this;
        }
        public Project Hold()
        {
            EnsureProjectActive();
            Status = ProjectStatus.OnHold;
            RegisterDomainEvents(new ProjectStatusUpdatedEvent(this));
            return this;
        }

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
            if (_tasks.Any(x => x.AssigneeId == userId))
                throw new InvalidOperationException($"User {userId} is currently on some tasks belong project {Id}");
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
        public Project AddTask(TaskItemTitle title, string description)
        {
            EnsureProjectActive();
            var oldProgress = Progress;
            _tasks.Add(TaskItem.Create(Id, title, description, TaskItemIndex.Create(_tasks.Count() + 1)));
            UpdateProjectStatus(oldProgress);
            return this;
        }
        public Project UpdateTask(TaskItemId taskItemId, TaskItemTitle title, string description)
        {
            EnsureProjectActive();
            FindTask(taskItemId).UpdateInfo(title, description);
            return this;
        }
        public Project ReorderTasks(List<TaskItemId> newOrders)
        {
            EnsureProjectActive();
            var currentOrders = _tasks.Select(x => x.Id).ToList();
            if (newOrders.Distinct().Count() != currentOrders.Count()
                || !newOrders.All(currentOrders.Contains)
                || !currentOrders.All(newOrders.Contains))
                throw new InvalidOperationException("New order list must contain exactly the same tasks as current list.");
            for (int newOverIndex = 0; newOverIndex < newOrders.Count(); newOverIndex++)
                FindTask(newOrders[newOverIndex]).UpdateOverIndex(TaskItemIndex.Create(newOverIndex + 1));
            return this;
        }
        public Project MarkDoneTask(TaskItemId taskItemId, bool isDone)
        {
            EnsureProjectActive();
            var oldProgress = Progress;
            FindTask(taskItemId).MarkDone(isDone);
            UpdateProjectStatus(oldProgress);
            return this;
        }
        public Project AssignTask(TaskItemId taskItemId, UserId userId)
        {
            EnsureProjectActive();
            FindMember(userId);
            var task = FindTask(taskItemId);
            task.Assign(userId);
            RegisterDomainEvents(new ProjectTaskAssignedEvent(this, task, userId));
            return this;
        }
        public Project UnAssignTask(TaskItemId taskItemId, UserId userId)
        {
            EnsureProjectActive();
            FindMember(userId);
            var task = FindTask(taskItemId);
            if (task.AssigneeId == default || task.AssigneeId != userId)
                throw new InvalidOperationException("User does not belong to this task");
            task.UnAssign();
            return this;
        }
        public Project RemoveTask(TaskItemId taskItemId)
        {
            EnsureProjectActive();
            var task = FindTask(taskItemId);
            if (task.AssigneeId != default)
                throw new InvalidOperationException($"User {task.AssigneeId} is currently on task");
            _tasks.Remove(FindTask(taskItemId));
            return this;
        }
        public Project AddTaskAttachment(TaskItemId taskItemId, AttachmentUrl fileUrl, UserId uploadBy)
        {
            EnsureProjectActive();
            var task = FindTask(taskItemId);
            task.AddAttachment(fileUrl, uploadBy);
            return this;
        }
        public Project RemoveTaskAttachment(TaskItemId taskItemId, AttachmentId attachmentId)
        {
            EnsureProjectActive();
            var task = FindTask(taskItemId);
            task.RemoveAttachment(attachmentId);
            return this;
        }
        //-----------------Issue-----------
        //----------------------------------
        private Issue FindIssue(IssueId issueId) =>
            _issues.FirstOrDefault(x => x.Id.Equals(issueId)) ?? throw new InvalidOperationException("Issue does not exists");
        public Project AddIssue(IssueContent content, IssueSeverity severity)
        {
            EnsureProjectActive();
            _issues.Add(Issue.Create(Id, content, severity));
            RegisterDomainEvents(new ProjectIssueAddedEvent(this));
            return this;
        }
        public Project UpdateIssue(IssueId issueId, IssueContent content, IssueSeverity severity)
        {
            EnsureProjectActive();
            FindIssue(issueId).UpdateInfo(content, severity);
            return this;
        }
        public Project ResolveIssue(IssueId issueId, IssueResolvedComment comment)
        {
            EnsureProjectActive();
            FindIssue(issueId).Resolve(comment);
            RegisterDomainEvents(new ProjectIssueResolvedEvent(this, issueId));
            return this;
        }
        public Project RemoveIssue(IssueId issueId)
        {
            EnsureProjectActive();
            _issues.Remove(FindIssue(issueId));
            return this;
        }
        public Project AddIssueAttachment(IssueId issueId, AttachmentUrl fileUrl, UserId uploadBy)
        {
            EnsureProjectActive();
            var issue = FindIssue(issueId);
            issue.AddAttachment(fileUrl, uploadBy);
            return this;
        }
        public Project RemoveIssueAttachment(IssueId issueId, AttachmentId attachmentId)
        {
            EnsureProjectActive();
            var issue = FindIssue(issueId);
            issue.RemoveAttachment(attachmentId);
            return this;
        }
    }
}
