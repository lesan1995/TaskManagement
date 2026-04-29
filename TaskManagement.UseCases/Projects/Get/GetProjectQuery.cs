using TaskManagement.UseCases.Projects.DTO;

namespace TaskManagement.UseCases.Projects.Get
{
    public record GetProjectQuery(ProjectId ProjectId) : IQuery<Result<ProjectDetailDTO>>;
}
