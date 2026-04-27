using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate
{
    public class Issue : EntityBase<Issue, IssueId>
    {
        public ProjectId ProjectId { get; private set; }
        public IssueContent Content { get; private set; }
        public IssueSeverity Severity { get; private set; }
        public bool IsResolved { get; private set; }
        public IssueResolvedComment ResolvedComment { get; private set; }
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
        internal void UpdateInfor(IssueContent content, IssueSeverity severity)
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
    }
}
