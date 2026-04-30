namespace TaskManagement.Core.ProjectAggregate
{
    public class TaskItem : EntityBase<TaskItem, TaskItemId>
    {
        public ProjectId ProjectId { get; private set; }
        public TaskItemTitle Title { get; private set; }
        public string Description { get; private set; }
        public bool IsDone { get; private set; }
        public UserId? AssigneeId { get; private set; }
        public TaskItemIndex OverIndex { get; private set; }
        private readonly List<Attachment> _attachments = new List<Attachment>();
        public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();
        private TaskItem(ProjectId projectId, TaskItemTitle title, string description, TaskItemIndex overIndex)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            OverIndex = overIndex;
            IsDone = false;
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
        internal void AddAttachment(AttachmentUrl fileUrl, UserId uploadBy) => _attachments.Add(Attachment.CreateForTask(fileUrl, uploadBy, Id));
        internal void RemoveAttachment(AttachmentId attachmentId)
        {
            var attachment = _attachments.FirstOrDefault(x => x.Id == attachmentId);
            if (attachment == null)
                throw new InvalidOperationException($"Attachment {attachmentId} not found in task {Id}");
            _attachments.Remove(attachment);
        }
    }
}