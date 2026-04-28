namespace TaskManagement.UseCases.Projects.Create
{
    public record CreateProjectCommand(ProjectName Name, string Description) : ICommand<Result<ProjectId>>;
}
