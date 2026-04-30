namespace TaskManagement.UseCases.Projects.Member.Add
{
    public record AddMemberCommand(ProjectId ProjectId, UserId UserId, ProjectMemberRole Role) : ICommand<Result<ProjectMemberDTO>>;
}
