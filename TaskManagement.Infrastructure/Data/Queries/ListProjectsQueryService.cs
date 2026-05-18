using TaskManagement.Core.ProjectAggregate;
using TaskManagement.UseCases;
using TaskManagement.UseCases.Projects.DTO;
using TaskManagement.UseCases.Projects.List;

namespace TaskManagement.Infrastructure.Data.Queries
{
    public class ListProjectsQueryService : IListProjectsQueryService
    {
        private readonly AppDbContext _db;
        public ListProjectsQueryService(AppDbContext db)
        {
            _db = db;
        }

        public Task<PagedResult<ProjectItemDTO>> ListAsync(ListProjectsFilter filter, int page, int perPage, CancellationToken ct)
        {
            var items = new List<ProjectItemDTO>();
            for(int i = 1; i <= 25; i++)
            {
                items.Add(new ProjectItemDTO(
                    ProjectId.Create(i),
                    ProjectName.Create("Project " + i),
                    ProjectDeadline.Create(DateTime.Now.AddDays(1)),
                    ProjectStatus.From(new Random().Next(1, 4)),
                    new Random().Next(1, 100),
                    (new Random().Next(1, 2) == 1)
                    ));
            }

            int totalCount = items.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
            var result = new PagedResult<ProjectItemDTO>(items, page, perPage, totalCount, totalPages);
            return Task.FromResult(result);
        }
    }
}
