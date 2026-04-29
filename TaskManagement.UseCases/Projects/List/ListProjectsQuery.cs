using TaskManagement.UseCases.Projects.DTO;

namespace TaskManagement.UseCases.Projects.List
{
    public record ListProjectsQuery(
        int? Page = 1, 
        int? PerPage = Constants.DEFAULT_PAGE_SIZE,
        string? Search = null,
        ProjectStatus? Status = null,
        string? Sort = "name",
        bool SortDesc = false
        ) : IQuery<Result<PagedResult<ProjectItemDTO>>>;
}
