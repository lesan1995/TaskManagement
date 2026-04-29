namespace TaskManagement.UseCases.Projects.Cancel
{
    public record CancelProjectCommand(ProjectId ProjectId) : ICommand<Result>;
}
