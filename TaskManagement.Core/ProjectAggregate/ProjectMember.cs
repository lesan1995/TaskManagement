using TaskManagement.Core.User;

namespace TaskManagement.Core.ProjectAggregate
{
    public class ProjectMember : EntityBaseWithoutId
    {
        public ProjectId ProjectId { get; private set; }
        public UserId UserId { get; private set; }
        public ProjectMemberRole Role { get; private set; }
        public DateTime JoinedAt { get; private set; }
        private ProjectMember(ProjectId projectId, UserId userId, ProjectMemberRole role)
        {
            ProjectId = projectId;
            UserId = userId;
            Role = role;
            JoinedAt = DateTime.UtcNow;
        }
        
        internal static ProjectMember Create(ProjectId projectId, UserId userId, ProjectMemberRole role) => new ProjectMember(projectId, userId, role);
        internal bool IsMemberShip => Role == ProjectMemberRole.Manager || Role == ProjectMemberRole.Member;
        internal bool IsManager => Role == ProjectMemberRole.Manager;
    }
}
