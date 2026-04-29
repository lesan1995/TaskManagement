namespace TaskManagement.UseCases.Projects.DTO
{
    public record ProjectMemberDTO(
        UserInfo UserInfo,
        ProjectMemberRole Role
        );
}
