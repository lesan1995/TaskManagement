using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Core.NotificationAggregate;

namespace TaskManagement.Infrastructure.Data.Config
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => NotificationId.Create(value))
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Content)
                .HasConversion(
                    content => content.Value,
                    value => NotificationContent.Create(value))
                .HasMaxLength(NotificationContent.MaxLength)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasMany(x => x.Users)
                .WithOne()
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);

            var userNavigation = builder.Metadata.FindNavigation(nameof(Notification.Users));
            userNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
