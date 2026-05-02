namespace TaskManagement.UseCases.Projects.Issuee.Update
{
    public record UpdateIssueCommand(ProjectId ProjectId, IssueId IssueId, IssueContent Content, IssueSeverity Severity) : ICommand<Result<IssueDTO>>;
}
