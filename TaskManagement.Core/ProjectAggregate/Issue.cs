using TaskManagement.Core.User;

namespace TaskManagement.Core.ProjectAggregate
{
    public class Issue : EntityBase<Issue, IssueId>
    {
        public ProjectId ProjectId { get; private set; }
        public IssueContent Content { get; private set; }
        public IssueSeverity Severity { get; private set; }
        public bool IsResolved { get; private set; }
        public IssueResolvedComment ResolvedComment { get; private set; }
        private readonly List<Attachment> _attachments = new List<Attachment>();
        public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();
        private Issue(ProjectId projectId, IssueContent content, IssueSeverity severity)
        {
            ProjectId = projectId;
            Content = content;
            Severity = severity;
            IsResolved = false;
            ResolvedComment = default!;
        }
        internal static Issue Create(ProjectId projectId, IssueContent content, IssueSeverity severity)
            => new(projectId, content, severity);
        internal void UpdateInfo(IssueContent content, IssueSeverity severity)
        {
            if (Content == content && Severity == severity) return;
            Content = content;
            Severity = severity;
        }
        internal void Resolve(IssueResolvedComment comment)
        {
            IsResolved = true;
            ResolvedComment = comment;
        }
        internal void AddAttachment(AttachmentUrl fileUrl, UserId uploadBy) => _attachments.Add(Attachment.CreateForIssue(fileUrl, uploadBy, Id));
        internal void RemoveAttachment(AttachmentId attachmentId)
        {
            var attachment = _attachments.FirstOrDefault(x => x.Id == attachmentId);
            if (attachment == null)
                throw new InvalidOperationException($"Attachment {attachmentId} not found in issue {Id}");
            _attachments.Remove(attachment);
        }
    }
}
