namespace TaskManagement.UseCases.Projects.Hold
{
    public class HoldProjectHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser) : ICommandHandler<HoldProjectCommand, Result>
    {
        public async ValueTask<Result> Handle(HoldProjectCommand command, CancellationToken ct)
        {
            if (!currentUser.IsManager)
                return Result.Forbidden("Only manager can be hold project");
            var spec = new ProjectByIdBasicSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();
            project.Hold();
            project.SetModified(currentUser.UserId);
            await repository.UpdateAsync(project, ct);
            return Result.Success();
        }
    }
}
