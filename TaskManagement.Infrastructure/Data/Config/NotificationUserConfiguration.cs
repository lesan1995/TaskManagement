using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Core.NotificationAggregate;

namespace TaskManagement.Infrastructure.Data.Config
{
    public class NotificationUserConfiguration : IEntityTypeConfiguration<NotificationUser>
    {
        public void Configure(EntityTypeBuilder<NotificationUser> builder)
        {
            builder.Property(x => x.UserId)
                .HasConversion(
                    x => x.Value,
                    value => UserId.Create(value))
                .IsRequired();

            builder.Property(x => x.IsRead)
                .IsRequired();
        }
    }
}
