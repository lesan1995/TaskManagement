namespace TaskManagement.UseCases.Projects.Issuee.Resolve
{
    public class ResolveIssueHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<ResolveIssueCommand, Result<IssueDTO>>
    {
        public async ValueTask<Result<IssueDTO>> Handle(ResolveIssueCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithIssueSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<IssueDTO>.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<IssueDTO>.Forbidden("You do not have permission to resolve issues");

            var issue = project.ResolveIssue(command.IssueId, command.Comment);
            project.SetModified(currentUser.UserId.ToString());
            await repository.UpdateAsync(project, ct);
            
            return Result<IssueDTO>.Success(issue.MapToIssueDto());
        }
    }
}
