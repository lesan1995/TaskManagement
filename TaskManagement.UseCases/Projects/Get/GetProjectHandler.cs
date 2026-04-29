using System.Net.Mail;
using TaskManagement.Core.ProjectAggregate.Specifications;
using TaskManagement.Core.User;
using TaskManagement.SharedKernel.User;
using TaskManagement.UseCases.Projects.DTO;

namespace TaskManagement.UseCases.Projects.Get
{
    public class GetProjectHandler(
        IRepository<Project> repository,
        IUserService userService) : IQueryHandler<GetProjectQuery, Result<ProjectDetailDTO>>
    {
        public async ValueTask<Result<ProjectDetailDTO>> Handle(GetProjectQuery query, CancellationToken ct)
        {
            var spec = new ProjectByIdSpec(query.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<ProjectDetailDTO>.NotFound();

            var memberInfos = await userService.GetUsersInfoAsync(
                project.Members.Select(x => x.UserId.ToString()), 
                ct);

            var projectDto = new ProjectDetailDTO(
                Id: project.Id,
                Name: project.Name,
                Description: project.Description,
                Deadline: project.Deadline,
                Status: project.Status,
                Progress: project.Progress,
                Members: project.Members.Select(m => new ProjectMemberDTO(
                    UserInfo: memberInfos.GetValueOrDefault(m.UserId.ToString()) ?? new UserInfo(),
                    Role: m.Role)).ToList(),
                Tasks: project.Tasks.Select(task => new TaskItemDTO(
                    Title: task.Title,
                    Description: task.Description,
                    IsDone: task.IsDone,
                    Assignee: task.AssigneeId != null ?
                        (memberInfos.TryGetValue(task.AssigneeId.Value.ToString(), out var userInfo) ? userInfo : null)
                        : null,
                    OverIndex: task.OverIndex,
                    Attachments: task.Attachments.Select(attachment => attachment.FileUrl).ToList())).ToList(),
                Issues: project.Issues.Select(issue => new IssueDTO(
                    Content:  issue.Content,
                    Severity:  issue.Severity,
                    IsResolved:  issue.IsResolved,
                    ResolvedComment: issue.ResolvedComment,
                    Attachments: issue.Attachments.Select(attachment => attachment.FileUrl).ToList())).ToList()
                );
            return Result<ProjectDetailDTO>.Success(projectDto);
        }
    }
}
