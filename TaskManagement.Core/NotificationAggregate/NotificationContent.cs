namespace TaskManagement.Core.NotificationAggregate
{
    public readonly record struct NotificationContent
    {
        public string Value { get; init; }
        public const int MaxLength = 500;
        public const int MaxLengthSummary = 10;
        private NotificationContent(string value) => Value = value;
        public string Summary() => Value.Substring(0, MaxLengthSummary);
        public static NotificationContent Create(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Notification content cannot be empty.");
            if (value.Length > MaxLength)
                throw new ArgumentException($"Notification content cannot be longer than {MaxLength} characters");
            return new NotificationContent(value);
        }
        public override string ToString() => Value;
    }
}
