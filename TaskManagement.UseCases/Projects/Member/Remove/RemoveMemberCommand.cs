namespace TaskManagement.UseCases.Projects.Member.Remove
{
    public record RemoveMemberCommand(ProjectId ProjectId, UserId UserId) : ICommand<Result>;
}
