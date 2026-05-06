using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Core.ProjectAggregate;

namespace TaskManagement.Infrastructure.Data.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => ProjectId.Create(value))
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasConversion(
                    name => name.Value,
                    value => ProjectName.Create(value))
                .HasMaxLength(ProjectName.MaxLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(x => x.Deadline)
                .HasConversion(
                    deadline => deadline.Value,
                    value => ProjectDeadline.Create(value))
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion(
                    status => status.Value,
                    value => ProjectStatus.From(value))
                .IsRequired();

            builder.Property(x => x.Progress)
                .IsRequired();

            builder.HasMany(x => x.Members)
                .WithOne()
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            var memberNavigation = builder.Metadata.FindNavigation(nameof(Project.Members));
            memberNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Tasks)
                .WithOne()
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            var taskNavigation = builder.Metadata.FindNavigation(nameof(Project.Tasks));
            taskNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Issues)
                .WithOne()
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            var issueNavigation = builder.Metadata.FindNavigation(nameof(Project.Issues));
            issueNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
