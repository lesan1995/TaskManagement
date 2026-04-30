namespace TaskManagement.UseCases.Projects.DTO
{
    public static class Mapper
    {
        public static ProjectDetailDTO MapToProjectDetailDto(this Project project, Dictionary<UserId, UserInfo> userInfos)
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
        public static ProjectMemberDTO MapToMemberDto(this ProjectMember member, UserInfo? userInfo)
        {
            return new ProjectMemberDTO(UserInfo: userInfo ?? new UserInfo { UserId = member.UserId, UserName = "Unknown User" }, Role: member.Role);
        }
        public static List<ProjectMemberDTO> MapToMemberDtos(this IReadOnlyCollection<ProjectMember> members, Dictionary<UserId, UserInfo> userInfos)
        {
            return members.Select(m => m.MapToMemberDto(userInfos.GetValueOrDefault(m.UserId))).ToList();
        }
        public static TaskItemDTO MapToTaskItemDto(this TaskItem task, UserInfo? userInfo = null)
        {
            return new TaskItemDTO(
                    Title: task.Title,
                    Description: task.Description,
                    IsDone: task.IsDone,
                    Assignee: task.AssigneeId.HasValue
                        ? (userInfo ?? new UserInfo { UserId = task.AssigneeId.Value, UserName = "Unknown User" })
                         : null,
                    OverIndex: task.OverIndex,
                    Attachments: task.Attachments.Select(attachment => attachment.FileUrl).ToList());
        }
        public static List<TaskItemDTO> MapToTaskItemDtos(this IReadOnlyCollection<TaskItem> tasks, Dictionary<UserId, UserInfo> userInfos)
        {
            return tasks.Select(task => task.MapToTaskItemDto(task.AssigneeId.HasValue ? userInfos.GetValueOrDefault(task.AssigneeId.Value) : null)).ToList();
        }
        public static IssueDTO MapToIssueDto(this Issue issue)
        {
            return new IssueDTO(
                    Content: issue.Content,
                    Severity: issue.Severity,
                    IsResolved: issue.IsResolved,
                    ResolvedComment: issue.ResolvedComment,
                    Attachments: issue.Attachments.Select(attachment => attachment.FileUrl).ToList()
                    );
        }
        public static List<IssueDTO> MapToIssueDtos(this IReadOnlyCollection<Issue> issues)
        {
            return issues.Select(issue => issue.MapToIssueDto()).ToList();
        }
    }
}
