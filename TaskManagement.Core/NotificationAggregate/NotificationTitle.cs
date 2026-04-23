namespace TaskManagement.Core.NotificationAggregate
{
    public readonly record struct NotificationTitle
    {
        public string Value { get; init; }
        public const int MaxLength = 100;
        private NotificationTitle(string value) => Value = value;
        public static NotificationTitle Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Notification title cannot be empty.");
            if (value.Length > MaxLength)
                throw new ArgumentException($"Notification title cannot be longer than {MaxLength} characters");
            return new NotificationTitle(value);
        }
        public override string ToString() => Value;
    }
}
