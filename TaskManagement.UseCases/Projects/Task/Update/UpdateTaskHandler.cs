namespace TaskManagement.UseCases.Projects.Task.Update
{
    public class UpdateTaskHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IUserService userService)
        : ICommandHandler<UpdateTaskCommand, Result<TaskItemDTO>>
    {
        public async ValueTask<Result<TaskItemDTO>> Handle(UpdateTaskCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<TaskItemDTO>.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<TaskItemDTO>.Forbidden("You do not have permission to update tasks");

            var task = project.UpdateTask(command.TaskItemId, command.Title, command.Description);
            var assigneeInfo = task.AssigneeId.HasValue
                ? (await userService.GetUserAsync(task.AssigneeId.Value, ct))
                : null;
            project.SetModified(currentUser.UserId);

            await repository.UpdateAsync(project, ct);

            return Result<TaskItemDTO>.Success(task.MapToTaskItemDto(assigneeInfo));
        }
    }
}
