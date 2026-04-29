namespace TaskManagement.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithMemberSpec : ProjectByIdBaseSpec
    {
        public ProjectByIdWithMemberSpec(ProjectId projectId) : base(projectId)
        {
            Query.Include(p => p.Members);
        }
    }
}
