using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Core.ProjectAggregate;

namespace TaskManagement.Infrastructure.Data.Config
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => IssueId.Create(value))
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Content)
                .HasConversion(
                    content => content.Value,
                    value => IssueContent.Create(value))
                .HasMaxLength(IssueContent.MaxLength)
                .IsRequired();

            builder.Property(x => x.Severity)
                .HasConversion(
                    severity => severity.Value,
                    value => IssueSeverity.From(value))
                .IsRequired();

            builder.Property(x => x.IsResolved)
                .IsRequired();

            builder.Property(x => x.ResolvedComment)
                .HasConversion(
                    resolvedComment => resolvedComment.HasValue ? resolvedComment.Value.Value : null,
                    value => value != null ? IssueResolvedComment.Create(value) : null)
                .HasMaxLength(IssueResolvedComment.MaxLength)
                .IsRequired(false);

            builder.HasMany(x => x.Attachments)
                .WithOne()
                .HasForeignKey(x => x.IssueId)
                .OnDelete(DeleteBehavior.Cascade);

            var attachmentNavigation = builder.Metadata.FindNavigation(nameof(Issue.Attachments));
            attachmentNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
