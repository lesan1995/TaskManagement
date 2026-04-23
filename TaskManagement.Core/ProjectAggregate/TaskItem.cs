using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate
{
    public class TaskItem(ProjectId projectId, TaskItemTitle title, string description, TaskItemIndex overIndex) : EntityBase<TaskItem, TaskItemId>
    {
        public ProjectId ProjectId { get; private set; } = projectId;
        public TaskItemTitle Title { get; private set; } = title;
        public string Description { get; private set; } = description;
        public bool IsDone { get; private set; } = false;
        public UserId AssigneeId { get; private set; } = default!;
        public TaskItemIndex OverIndex { get; private set; } = overIndex;
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
        public TaskItem UpdateOverIndex(TaskItemIndex overIndex)
        {
            OverIndex = overIndex;
            return this;
        }
    }
}
