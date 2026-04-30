namespace TaskManagement.UseCases.Projects.Member.Add
{
    public class AddMemberHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IUserService userService)
        : ICommandHandler<AddMemberCommand, Result<ProjectMemberDTO>>
    {
        public async ValueTask<Result<ProjectMemberDTO>> Handle(AddMemberCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithMemberSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<ProjectMemberDTO>.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<ProjectMemberDTO>.Forbidden("You do not have permission to add memmbers");

            var newMember = project.AddMember(command.UserId, command.Role);
            var newMemberInfo = await userService.GetUserAsync(command.UserId, ct);

            await repository.UpdateAsync(project);

            return Result<ProjectMemberDTO>.Success(newMember.MapToMemberDto(newMemberInfo));
        }
    }
}
