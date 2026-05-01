namespace TaskManagement.UseCases.Projects.Task.Reorder
{
    public class ReorderTaskHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<ReorderTaskCommand, Result>
    {
        public async ValueTask<Result> Handle(ReorderTaskCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result.Forbidden("You do not have permission to reorder tasks");

            project.ReorderTasks(command.NewOrders);
            project.SetModified(currentUser.UserId.ToString());

            await repository.UpdateAsync(project, ct);

            return Result.Success();
        }
    }
}
