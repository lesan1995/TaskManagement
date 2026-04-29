namespace TaskManagement.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithTaskSpec : ProjectByIdBaseSpec
    {
        public ProjectByIdWithTaskSpec(ProjectId projectId) : base(projectId)
        {
            Query.Include(p => p.Tasks).ThenInclude(t => t.Attachments);
        }
    }
}
