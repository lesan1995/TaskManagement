using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Core.ProjectAggregate;
using TaskManagement.SharedKernel.User;

namespace TaskManagement.Infrastructure.Data.Config
{
    public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
    {
        public void Configure(EntityTypeBuilder<ProjectMember> builder)
        {
            builder.Property(x => x.UserId)
                .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value))
                .IsRequired();

            builder.Property(x => x.Role)
                .HasConversion(
                    role => role.Value,
                    value => ProjectMemberRole.From(value))
                .IsRequired();

            builder.Property(x => x.JoinedAt)
                .IsRequired();
        }
    }
}
