namespace TaskManagement.UseCases.Projects.Task.Reorder
{
    public record ReorderTaskCommand(ProjectId ProjectId, List<TaskItemId> NewOrders) : ICommand<Result>;
}