using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

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
            IsDone = false;
            AssigneeId = default!;
            OverIndex = overIndex;
        }
        public static TaskItem Create(ProjectId projectId, TaskItemTitle title, string description, TaskItemIndex overIndex)
            => new TaskItem(projectId, title, description, overIndex);
        public TaskItem Done()
        {
            IsDone = true;
            return this;
        }
        public TaskItem Assign(UserId userId)
        {
            AssigneeId = userId;
            return this;
        }
        public TaskItem UpdateTitle(TaskItemTitle title)
        {
            Title = title;
            return this;
        }
        public TaskItem UpdateDescription(string description)
        {
            Description = description;
            return this;
        }
        public void UpdateOverIndex(TaskItemIndex overIndex)
        {
            OverIndex = overIndex;
            return this;
        }
    }
}