using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.ProjectAggregate
{
    public class ProjectMember(ProjectId projectId, UserId userId, ProjectMemberRole role) : EntityBaseWithoutId
    {
        public ProjectId ProjectId { get; private set; } = projectId;
        public UserId UserId { get; private set; } = userId;
        public ProjectMemberRole Role { get; private set; } = role;
        public DateTime JoinedAt { get; private set; } = DateTime.UtcNow;
    }
}
