namespace TaskManagement.UseCases.Projects.Task.Assign
{
    public class AssignTaskHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IUserService userService)
        : ICommandHandler<AssignTaskCommand, Result<TaskItemDTO>>
    {
        public async ValueTask<Result<TaskItemDTO>> Handle(AssignTaskCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<TaskItemDTO>.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<TaskItemDTO>.Forbidden("You do not have permission to assign or unassign tasks");

            var assignUserInfo = await userService.GetUserAsync(command.UserId, ct);

            TaskItem task;
            if(command.IsAssign) task = project.AssignTask(command.TaskId, command.UserId);
            else task = project.UnAssignTask(command.TaskId, command.UserId);

            project.SetModified(currentUser.UserId.ToString());

            await repository.UpdateAsync(project, ct);

            return Result<TaskItemDTO>.Success(task.MapToTaskItemDto(assignUserInfo));
        }
    }
}
