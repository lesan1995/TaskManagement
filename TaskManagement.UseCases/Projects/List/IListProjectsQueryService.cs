namespace TaskManagement.UseCases.Projects.List
{
    public interface IListProjectsQueryService
    {
        Task<PagedResult<ProjectItemDTO>> ListAsync(ListProjectsFilter filter, int page, int perPage, CancellationToken ct);
    }
}
