namespace TaskManagement.UseCases.Projects.Issuee.Add
{
    public class AddIssueHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<AddIssueCommand, Result<IssueDTO>>
    {
        public async ValueTask<Result<IssueDTO>> Handle(AddIssueCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithIssueSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<IssueDTO>.NotFound();

            bool hasPermission = project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<IssueDTO>.Forbidden("You do not have permission to add issues");

            var newIssue = project.AddIssue(command.Content, command.Severity);
            project.SetModified(currentUser.UserId.ToString());
            await repository.UpdateAsync(project, ct);
            
            return Result<IssueDTO>.Success(newIssue.MapToIssueDto());
        }
    }
}
