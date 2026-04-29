using TaskManagement.SharedKernel.User;

namespace TaskManagement.UseCases.Projects.DTO
{
    public static class Mapper
    {
        public static ProjectDetailDTO MapToProjectDetailDto(this Project project, Dictionary<string, UserInfo> userInfos)
        {
            return new ProjectDetailDTO(
                Id: project.Id,
                Name: project.Name,
                Description: project.Description,
                Deadline: project.Deadline,
                Status: project.Status,
                Progress: project.Progress,
                Members: project.Members.MapToMemberDtos(userInfos),
                Tasks: project.Tasks.MapToTaskItemDtos(userInfos),
                Issues: project.Issues.MapToIssueDtos()
                );
        }
        public static List<ProjectMemberDTO> MapToMemberDtos(this IReadOnlyCollection<ProjectMember> members, Dictionary<string, UserInfo> userInfos)
        {
            return members.Select(m => new ProjectMemberDTO(
                    UserInfo: userInfos.GetValueOrDefault(m.UserId.ToString()) ?? new UserInfo { UserId = m.UserId.ToString(), UserName = "Unknown User" },
                    Role: m.Role)).ToList();
        }
        public static List<TaskItemDTO> MapToTaskItemDtos(this IReadOnlyCollection<TaskItem> tasks, Dictionary<string, UserInfo> userInfos)
        {
            return tasks.Select(task => new TaskItemDTO(
                    Title: task.Title,
                    Description: task.Description,
                    IsDone: task.IsDone,
                    Assignee: task.AssigneeId.HasValue ? userInfos.GetValueOrDefault(task.AssigneeId.Value.ToString()) : null,
                    OverIndex: task.OverIndex,
                    Attachments: task.Attachments.Select(attachment => attachment.FileUrl).ToList()
                    )).ToList();
        }
        public static List<IssueDTO> MapToIssueDtos(this IReadOnlyCollection<Issue> issues)
        {
            return issues.Select(issue => new IssueDTO(
                    Content: issue.Content,
                    Severity: issue.Severity,
                    IsResolved: issue.IsResolved,
                    ResolvedComment: issue.ResolvedComment,
                    Attachments: issue.Attachments.Select(attachment => attachment.FileUrl).ToList()
                    )).ToList();
        }
    }
}
