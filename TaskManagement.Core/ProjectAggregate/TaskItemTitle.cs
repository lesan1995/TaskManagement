namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct TaskItemTitle
    {
        public string Value { get; init; }
        public const int MaxLength = 100;
        private TaskItemTitle(string value) => Value = value;
        public static TaskItemTitle Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Task title cannot be empty.");
            if (value.Length > MaxLength)
                throw new ArgumentException($"Task title cannot be longer than {MaxLength} characters");
            return new TaskItemTitle(value);
        }
        public override string ToString() => Value;
    }
}
