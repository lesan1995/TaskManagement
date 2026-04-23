using TaskManagement.SharedKernel.Utils;

namespace TaskManagement.Core.NotificationAggregate
{
    public readonly record struct NotificationTime
    {
        public DateTime Value { get; init; }
        private NotificationTime(DateTime value) => Value = value;
        public static NotificationTime Create() => new NotificationTime(DateTime.UtcNow);
        public override string ToString() => Value.ToTimeAgo();
    }
}
