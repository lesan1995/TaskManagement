namespace TaskManagement.UseCases.Projects.Issuee.Remove
{
    public record RemoveIssueCommand(ProjectId ProjectId, IssueId IssueId) : ICommand<Result>;
}