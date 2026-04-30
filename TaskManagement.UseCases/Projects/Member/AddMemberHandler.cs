namespace TaskManagement.UseCases.Projects.Member
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

            if(!currentUser.IsManager || !project.IsProjectManager(currentUser.UserId))
                return Result<ProjectMemberDTO>.Forbidden("You do not have permission to add memmbers");

            var newMember = project.AddMember(command.userId, command.role);
            var newMemberInfo = await userService.GetUserAsync(command.userId, ct);

            await repository.UpdateAsync(project);

            return Result<ProjectMemberDTO>.Success(newMember.MapToMemberDto(newMemberInfo));
        }
    }
}
