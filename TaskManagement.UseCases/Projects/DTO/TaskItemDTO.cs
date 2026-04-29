using TaskManagement.SharedKernel.User;

namespace TaskManagement.UseCases.Projects.DTO
{
    public record TaskItemDTO(
        TaskItemTitle Title,
        string Description,
        bool IsDone,
        UserInfo? Assignee,
        TaskItemIndex OverIndex,
        IReadOnlyList<AttachmentUrl> Attachments
        );
}
