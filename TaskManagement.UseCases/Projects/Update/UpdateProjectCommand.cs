namespace TaskManagement.UseCases.Projects.Update
{
    public record UpdateProjectCommand(
        ProjectId ProjectId, 
        ProjectName? Name = null, 
        string? Description = null, 
        ProjectDeadline? Deadline = null) : ICommand<Result>
    {
    }
}
