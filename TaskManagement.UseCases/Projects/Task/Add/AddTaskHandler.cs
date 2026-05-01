namespace TaskManagement.UseCases.Projects.Task.Add
{
    public class AddTaskHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<AddTaskCommand, Result<TaskItemDTO>>
    {
        public async ValueTask<Result<TaskItemDTO>> Handle(AddTaskCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<TaskItemDTO>.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<TaskItemDTO>.Forbidden("You do not have permission to add tasks");

            var newTask = project.AddTask(command.Title, command.Description);
            project.SetModified(currentUser.UserId.ToString());
            await repository.UpdateAsync(project, ct);
            
            return Result<TaskItemDTO>.Success(newTask.MapToTaskItemDto());
        }
    }
}
