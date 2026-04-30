namespace TaskManagement.UseCases.Projects.Member.Remove
{
    public record UpdateMemberRoleCommand(ProjectId ProjectId, UserId UserId, ProjectMemberRole Role) : ICommand<Result>;
}
