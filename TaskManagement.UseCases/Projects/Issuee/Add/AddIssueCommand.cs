namespace TaskManagement.UseCases.Projects.Issuee.Add
{
    public record AddIssueCommand(ProjectId ProjectId, IssueContent Content, IssueSeverity Severity) : ICommand<Result<IssueDTO>>;
}
