namespace TaskManagement.UseCases.Projects.List
{
    public record ListProjectsFilter(
        string UserId,
        bool IsManager,
        string? Search = null,
        ProjectStatus? Status = null,
        string? SortBy = "name",
        bool SortDesc = false,
        bool IncludeDeleted = false
        );
}
