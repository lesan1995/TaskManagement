using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Core.ProjectAggregate;
using TaskManagement.SharedKernel.User;

namespace TaskManagement.Infrastructure.Data.Config
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => TaskItemId.Create(value))
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .HasConversion(
                    title => title.Value,
                    value => TaskItemTitle.Create(value))
                .HasMaxLength(TaskItemTitle.MaxLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(x => x.IsDone)
                .IsRequired();

            builder.Property(x => x.AssigneeId)
                .HasConversion(
                    assigneeId => assigneeId.HasValue ? assigneeId.Value.Value : (int?)null,
                    value => value.HasValue ? UserId.Create(value.Value) : null)
                .IsRequired(false);

            builder.Property(x => x.OverIndex)
                .HasConversion(
                    overIndex => overIndex.Value,
                    value => TaskItemIndex.Create(value))
                .IsRequired();

            builder.HasMany(x => x.Attachments)
                .WithOne()
                .HasForeignKey(at => at.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            var attachmentNavigation = builder.Metadata.FindNavigation(nameof(TaskItem.Attachments));
            attachmentNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
