namespace TaskManagement.UseCases.Projects.Delete
{
    public class DeleteProjectHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser) : ICommandHandler<DeleteProjectCommand, Result>
    {
        public async ValueTask<Result> Handle(DeleteProjectCommand command, CancellationToken ct)
        {
            if (!currentUser.IsManager)
                return Result.Forbidden("Only manager can be delete project");
            var spec = new ProjectByIdBasicSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();
            project.SoftDelete(currentUser.UserId.ToString());
            await repository.UpdateAsync(project, ct);
            return Result.Success();
        }
    }
}
