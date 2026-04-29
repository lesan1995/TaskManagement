namespace TaskManagement.UseCases.Projects.Cancel
{
    public class CancelProjectHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser) : ICommandHandler<CancelProjectCommand, Result>
    {
        public async ValueTask<Result> Handle(CancelProjectCommand command, CancellationToken ct)
        {
            if (!currentUser.IsManager)
                return Result.Forbidden("Only managers can cancel project");
            var project = await repository.GetByIdAsync(command.ProjectId, ct);
            if (project == null)
                return Result.NotFound();
            project.Cancel();
            project.SetModified(currentUser.UserId);
            return Result.Success();
        }
    }
}