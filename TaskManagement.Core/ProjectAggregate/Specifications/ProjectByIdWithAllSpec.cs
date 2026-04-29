namespace TaskManagement.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithAllSpec : ProjectByIdBaseSpec
    {
        public ProjectByIdWithAllSpec(ProjectId projectId) : base(projectId)
        {
            Query
            .Include(p => p.Members)
            .Include(p => p.Tasks)
                .ThenInclude(t => t.Attachments)
            .Include(p => p.Issues)
                .ThenInclude(i => i.Attachments);
        }
    }
}
