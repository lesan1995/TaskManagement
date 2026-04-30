namespace TaskManagement.UseCases.Projects.Member
{
    public record AddMemberCommand(ProjectId ProjectId, UserId UserId, ProjectMemberRole Role) : ICommand<Result<ProjectMemberDTO>>;
}
