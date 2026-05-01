namespace TaskManagement.UseCases.Projects.Task.Mark
{
    public class MarkTaskHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<MarkTaskCommand, Result<MarkTaskResultDTO>>
    {
        public async ValueTask<Result<MarkTaskResultDTO>> Handle(MarkTaskCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<MarkTaskResultDTO>.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId) 
                || project.IsTaskOwner(command.TaskItemId, currentUser.UserId);
            if (!hasPermission)
                return Result<MarkTaskResultDTO>.Forbidden("You do not have permission to mark tasks");

            project.MarkDoneTask(command.TaskItemId, command.IsDone);
            
            project.SetModified(currentUser.UserId.ToString());

            await repository.UpdateAsync(project, ct);

            return Result<MarkTaskResultDTO>.Success(new MarkTaskResultDTO(project.Progress, project.Status));
        }
    }
}
