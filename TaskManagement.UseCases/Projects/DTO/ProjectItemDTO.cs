namespace TaskManagement.UseCases.Projects.DTO
{
    public record ProjectItemDTO(ProjectId Id, ProjectName Name, ProjectDeadline Deadline, ProjectStatus Status, int Progress, bool HasIssue);
}
