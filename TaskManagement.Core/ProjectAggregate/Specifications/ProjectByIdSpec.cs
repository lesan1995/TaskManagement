using Ardalis.Specification;

namespace TaskManagement.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdSpec : Specification<Project>
    {
        public ProjectByIdSpec(ProjectId projectId) =>
            Query
            .Include(p => p.Members)
            .Include(p => p.Tasks)
                .ThenInclude(t => t.Attachments)
            .Include(p => p.Issues)
                .ThenInclude(i => i.Attachments)
            .Where(project => project.Id == projectId && !project.IsDeleted);
    }
}
