namespace TaskManagement.UseCases.Projects.Member.Remove
{
    public class RemoveMemberHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<RemoveMemberCommand, Result>
    {
        public async ValueTask<Result> Handle(RemoveMemberCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithMemberSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<ProjectMemberDTO>.Forbidden("You do not have permission to remove members");

            project.RemoveMember(command.UserId);
            project.SetModified(currentUser.UserId);
            await repository.UpdateAsync(project, ct);

            return Result.Success();
        }
    }
}
