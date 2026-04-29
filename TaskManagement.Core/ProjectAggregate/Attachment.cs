using TaskManagement.Core.User;

namespace TaskManagement.Core.ProjectAggregate
{
    public class Attachment : EntityBase<Attachment, AttachmentId>
    {
        public AttachmentUrl FileUrl { get; init; }
        public UserId UploadBy { get; init; }
        public TaskItemId? TaskId { get; init; }
        public IssueId? IssueId { get; init; }
        private Attachment(AttachmentUrl fileUrl, UserId uploadBy, TaskItemId? taskId, IssueId? issueId)
        {
            if ((taskId == null && issueId == null)
                || (taskId != null && issueId != null))
                throw new ArgumentException("Attachment must be belong to just one issue or just one task");
            FileUrl = fileUrl;
            UploadBy = uploadBy;
            TaskId = taskId;
            IssueId = issueId;
        }
        public static Attachment CreateForTask(AttachmentUrl fileUrl, UserId uploadBy, TaskItemId taskId) => new Attachment(fileUrl, uploadBy, taskId, null);
        public static Attachment CreateForIssue(AttachmentUrl fileUrl, UserId uploadBy, IssueId issueId) => new Attachment(fileUrl, uploadBy, null, issueId);
    }
}
