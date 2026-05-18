namespace TaskManagement.UseCases.Projects.List
{
    public class ListProjectsHandler(
        IListProjectsQueryService query,
        ICurrentUserService currentUser) : IQueryHandler<ListProjectsQuery, Result<PagedResult<ProjectItemDTO>>>
    {
        public async ValueTask<Result<PagedResult<ProjectItemDTO>>> Handle(ListProjectsQuery request, CancellationToken ct)
        {
            var filter = new ListProjectsFilter(
                UserId: currentUser.UserId,
                IsManager: currentUser.IsManager,
                Search: request.Search,
                Status: request.Status,
                SortBy: request.SortBy,
                SortDesc: request.SortDesc,
                IncludeDeleted: false
            );

            var result = await query.ListAsync(filter, request.Page ?? 1, request.PerPage ?? Constants.DEFAULT_PAGE_SIZE, ct);

            return Result<PagedResult<ProjectItemDTO>>.Success(result);
        }
    }
}
