namespace TaskManagement.UseCases.Projects.DTO
{
    public record ProjectDetailDTO(
        ProjectId Id,
        ProjectName Name,
        string Description,
        ProjectDeadline Deadline,
        ProjectStatus Status,
        int Progress,
        IReadOnlyList<ProjectMemberDTO> Members,
        IReadOnlyList<TaskItemDTO> Tasks,
        IReadOnlyList<IssueDTO> Issues
        );
}
