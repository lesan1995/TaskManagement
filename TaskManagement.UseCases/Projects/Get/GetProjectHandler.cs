namespace TaskManagement.UseCases.Projects.Get
{
    public class GetProjectHandler(
        IRepository<Project> repository,
        IUserService userService) : IQueryHandler<GetProjectQuery, Result<ProjectDetailDTO>>
    {
        public async ValueTask<Result<ProjectDetailDTO>> Handle(GetProjectQuery query, CancellationToken ct)
        {
            var spec = new ProjectByIdWithAllSpec(query.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<ProjectDetailDTO>.NotFound();

            var memberInfos = await userService.GetUsersInfoAsync(project.Members.Select(x => x.UserId.ToString()), ct);

            var projectDto = project.MapToProjectDetailDto(memberInfos);

            return Result<ProjectDetailDTO>.Success(projectDto);
        }
    }
}
