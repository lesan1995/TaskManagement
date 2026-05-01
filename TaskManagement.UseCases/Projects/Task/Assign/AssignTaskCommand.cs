namespace TaskManagement.UseCases.Projects.Task.Assign
{
    public record AssignTaskCommand(ProjectId ProjectId, TaskItemId TaskId, UserId UserId, bool IsAssign) : ICommand<Result<TaskItemDTO>>;
}