namespace TaskManagement.UseCases.Projects.Delete
{
    public record DeleteProjectCommand(ProjectId ProjectId) : ICommand<Result>;
}
