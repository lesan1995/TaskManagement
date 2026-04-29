namespace TaskManagement.Core.ProjectAggregate.Specifications
{
    public abstract class ProjectByIdBaseSpec : Specification<Project>
    {
        public ProjectByIdBaseSpec(ProjectId projectId) =>
            Query
            .Where(p => p.Id == projectId && !p.IsDeleted);
    }
}
