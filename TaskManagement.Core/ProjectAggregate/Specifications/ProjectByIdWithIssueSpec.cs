namespace TaskManagement.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithIssueSpec : ProjectByIdBaseSpec
    {
        public ProjectByIdWithIssueSpec(ProjectId projectId) : base(projectId)
        {
            Query.Include(p => p.Issues).ThenInclude(t => t.Attachments);
        }
    }
}
