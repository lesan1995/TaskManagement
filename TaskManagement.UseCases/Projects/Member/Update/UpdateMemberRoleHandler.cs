namespace TaskManagement.UseCases.Projects.Member.Remove
{
    public class UpdateMemberRoleHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<UpdateMemberRoleCommand, Result>
    {
        public async ValueTask<Result> Handle(UpdateMemberRoleCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithMemberSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<ProjectMemberDTO>.Forbidden("You do not have permission to update role of members");

            project.UpdateMemberRole(command.UserId, command.Role);

            await repository.UpdateAsync(project);

            return Result.Success();
        }
    }
}
