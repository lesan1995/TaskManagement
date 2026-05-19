using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Core.ProjectAggregate;

namespace TaskManagement.Infrastructure.Data.Config
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => AttachmentId.Create(value))
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FileUrl)
                .HasConversion(
                    fileUrl => fileUrl.Value,
                    value => AttachmentUrl.Create(value))
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.UploadBy)
                .HasConversion(
                    uploadBy => uploadBy.Value,
                    value => UserId.Create(value))
                .IsRequired();

            builder.Property(x => x.TaskId)
                .HasConversion(
                    taskId => taskId.HasValue ? taskId.Value.Value : (int?)null,
                    value => value.HasValue ? TaskItemId.Create(value.Value) : null
                    )
                .IsRequired(false);

            builder.Property(x => x.IssueId)
                .HasConversion(
                    issueId => issueId.HasValue ? issueId.Value.Value : (int?)null,
                    value => value.HasValue ? IssueId.Create(value.Value) : null)
                .IsRequired(false);

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Attachment_OnlyOneParent",
                "([TaskId] IS NOT NULL AND [IssueId] IS NULL) "+
                "OR ([TaskId] IS NULL AND [IssueId] IS NOT NULL)"));

        }
    }
}
