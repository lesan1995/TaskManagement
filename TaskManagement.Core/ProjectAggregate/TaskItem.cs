using TaskManagement.Core.User;

namespace TaskManagement.Core.ProjectAggregate
{
    public class TaskItem : EntityBase<TaskItem, TaskItemId>
    {
        public ProjectId ProjectId { get; private set; }
        public TaskItemTitle Title { get; private set; }
        public string Description { get; private set; }
        public bool IsDone { get; private set; }
        public UserId AssigneeId { get; private set; }
        public TaskItemIndex OverIndex { get; private set; }
        private TaskItem(ProjectId projectId, TaskItemTitle title, string description, TaskItemIndex overIndex)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            OverIndex = overIndex;
            IsDone = false;
            AssigneeId = default!;
        }
        internal static TaskItem Create(ProjectId projectId, TaskItemTitle title, string description, TaskItemIndex overIndex)
            => new TaskItem(projectId, title, description, overIndex);
        internal void UpdateInfo(TaskItemTitle title, string description)
        {
            if(Title == title && Description == description) return;
            Title = title;
            Description = description;
        }
        internal void MarkDone(bool isDone) => IsDone = isDone;
        internal void Assign(UserId userId) => AssigneeId = userId;
        internal void UnAssign() => AssigneeId = default!;
        internal void UpdateOverIndex(TaskItemIndex overIndex) => OverIndex = overIndex;
    }
}