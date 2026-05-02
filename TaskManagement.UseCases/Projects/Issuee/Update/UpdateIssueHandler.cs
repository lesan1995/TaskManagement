namespace TaskManagement.UseCases.Projects.Issuee.Update
{
    public class UpdateIssueHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<UpdateIssueCommand, Result<IssueDTO>>
    {
        public async ValueTask<Result<IssueDTO>> Handle(UpdateIssueCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithIssueSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<IssueDTO>.NotFound();

            bool hasPermission = project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<IssueDTO>.Forbidden("You do not have permission to update issues");

            var issue = project.UpdateIssue(command.IssueId, command.Content, command.Severity);
            project.SetModified(currentUser.UserId.ToString());
            await repository.UpdateAsync(project, ct);
            
            return Result<IssueDTO>.Success(issue.MapToIssueDto());
        }
    }
}
