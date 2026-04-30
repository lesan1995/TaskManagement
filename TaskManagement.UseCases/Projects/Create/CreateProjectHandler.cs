namespace TaskManagement.UseCases.Projects.Create
{
    public class CreateProjectHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser)
        : ICommandHandler<CreateProjectCommand, Result<ProjectId>>
    {
        public async ValueTask<Result<ProjectId>> Handle(CreateProjectCommand command, CancellationToken ct)
        {
            if (!currentUser.IsManager)
                return Result<ProjectId>.Forbidden("Only managers can create Projects");
            var newProject = Project.Create(command.Name, command.Description);
            newProject.SetCreated(currentUser.UserId);
            var createdItem = await repository.AddAsync(newProject, ct);
            return Result<ProjectId>.Success(createdItem.Id);
        }
    }
}
