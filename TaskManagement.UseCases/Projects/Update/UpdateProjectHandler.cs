namespace TaskManagement.UseCases.Projects.Update
{
    public class UpdateProjectHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<UpdateProjectCommand, Result>
    {
        public async ValueTask<Result> Handle(UpdateProjectCommand command, CancellationToken ct)
        {
            if (!currentUser.IsManager)
                return Result.Forbidden("Only managers can update project");

            var spec = new ProjectByIdBasicSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();

            var hasChanges = false;
            var newName = command.Name ?? project.Name;
            var newDescription = command.Description ?? project.Description;

            if (project.Name != newName || project.Description != newDescription)
            {
                project.UpdateInfo(newName, newDescription);
                hasChanges = true;
            }

            if (command.Deadline.HasValue && project.Deadline != command.Deadline)
            {
                project.SetDeadline(command.Deadline.Value);
                hasChanges = true;
            }

            if (!hasChanges) return Result.Success();

            project.SetModified(currentUser.UserId);

            await repository.UpdateAsync(project, ct);
            return Result.Success();
        }
    }
}
