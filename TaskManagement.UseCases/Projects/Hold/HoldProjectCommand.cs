namespace TaskManagement.UseCases.Projects.Hold
{
    public record HoldProjectCommand(ProjectId ProjectId) : ICommand<Result>;
}
