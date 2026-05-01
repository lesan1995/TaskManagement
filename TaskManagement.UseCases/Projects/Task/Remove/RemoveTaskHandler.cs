namespace TaskManagement.UseCases.Projects.Task.Remove
{
    public class RemoveTaskHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<RemoveTaskCommand, Result>
    {
        public async ValueTask<Result> Handle(RemoveTaskCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<MarkTaskResultDTO>.Forbidden("You do not have permission to remove tasks");

            project.RemoveTask(command.TaskItemId);
            
            project.SetModified(currentUser.UserId.ToString());

            await repository.UpdateAsync(project, ct);

            return Result.Success();
        }
    }
}
