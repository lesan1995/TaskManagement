namespace TaskManagement.Core.NotificationAggregate
{
    public readonly record struct NotificationId
    {
        public int Value { get; init; }
        private NotificationId(int value) => Value = value;
        public static NotificationId Create(int value)
        {
            if (value < 0) throw new ArgumentException("Notification Id cannot be positive");
            return new NotificationId(value);
        }
        public override string ToString() => Value.ToString();
        public static implicit operator int(NotificationId id) => id.Value;
    }
}
