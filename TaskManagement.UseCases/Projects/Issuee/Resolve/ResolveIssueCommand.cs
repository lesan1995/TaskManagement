namespace TaskManagement.UseCases.Projects.Issuee.Resolve
{
    public record ResolveIssueCommand(ProjectId ProjectId, IssueId IssueId, IssueResolvedComment Comment) : ICommand<Result<IssueDTO>>;
}
