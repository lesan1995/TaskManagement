namespace TaskManagement.UseCases.Projects.Member
{
    public record AddMemberCommand(ProjectId ProjectId, UserId userId, ProjectMemberRole role) : ICommand<Result<ProjectMemberDTO>>;
}
