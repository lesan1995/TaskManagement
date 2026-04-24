using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate
{
    public class Issue : EntityBase<Issue, IssueId>
    {
        public ProjectId ProjectId { get; private set; }
        public IssueTitle Title { get; private set; }
        public string Description { get; private set; }
        public IssueSeverity Severity { get; private set; }
        public bool IsResolved { get; private set; }
        public IssueResolvedComment ResolvedComment { get; private set; }
        private Issue(ProjectId projectId, IssueTitle title, string description, IssueSeverity severity)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            Severity = severity;
            IsResolved = false;
            ResolvedComment = default!;
        }
        public static Issue Create(ProjectId projectId, IssueTitle title, string description, IssueSeverity severity)
            => new(projectId, title, description, severity);
        public Issue UpdateTitle(IssueTitle title)
        {
            Title = title;
            return this;
        }
        public Issue UpdateDescription(string description)
        {
            Description = description;
            return this;
        }
        public Issue UpdateSeverity(IssueSeverity severity)
        {
            Severity = severity;
            return this;
        }
        public Issue Resolve(string comment)
        {
            IsResolved = true;
            return this;
        }
    }
}
