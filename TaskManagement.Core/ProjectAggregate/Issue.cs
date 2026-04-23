using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate
{
    public class Issue(ProjectId projectId, IssueTitle title, string description, IssueSeverity severity) : EntityBase<Issue, IssueId>
    {
        public ProjectId ProjectId { get; private set; } = projectId;
        public IssueTitle Title { get; private set; } = title;
        public string Description { get; private set; } = description;
        public IssueSeverity Severity { get; private set; } = severity;
        public bool IsResolved { get; private set; } = false;
        public IssueResolvedComment ResolvedComment { get; private set; } = default!;
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
