namespace TaskManagement.Core.ProjectAggregate.Events
{
    public sealed class ProjectIssueResolvedEvent(Project project, IssueId issueId) : DomainEventBase
    {
        public Project Project { get; init; } = project;
        public IssueId IssueId { get; init; } = issueId;
    }
}
