namespace TaskManagement.UseCases.Projects.Member.Update
{
    public record UpdateMemberRoleCommand(ProjectId ProjectId, UserId UserId, ProjectMemberRole Role) : ICommand<Result>;
}
