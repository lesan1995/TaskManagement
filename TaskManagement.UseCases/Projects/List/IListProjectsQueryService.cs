using TaskManagement.UseCases.Projects.DTO;

namespace TaskManagement.UseCases.Projects.List
{
    public interface IListProjectsQueryService
    {
        Task<PagedResult<ProjectItemDTO>> ListAsync(ListProjectsFilter filter, CancellationToken ct);
    }
}
