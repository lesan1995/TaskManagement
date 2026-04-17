namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct ProjectStatus
    {
        public int Value { get; init; }
        public string Name { get; init; }
        private ProjectStatus(int value, string name)
        {
            Value = value;
            Name = name;
        }
        public static readonly ProjectStatus NotStarted = new(0, nameof(NotStarted));
        public static readonly ProjectStatus InProgress = new(1, nameof(InProgress));
        public static readonly ProjectStatus OnHold = new(2, nameof(OnHold));
        public static readonly ProjectStatus Completed = new(3, nameof(Completed));
        public static readonly ProjectStatus Cancelled = new(4, nameof(Cancelled));

        public static ProjectStatus From(int value) => value switch
        {
            0 => NotStarted,
            1 => InProgress,
            2 => OnHold,
            3 => Completed,
            4 => Cancelled,
            _ => throw new ArgumentException($"Invalid Project status: {value}")
        };

        public bool IsActive() => this == InProgress || this == OnHold;
        public bool IsFinished() => this == Completed;
        public bool IsCancelled() => this == Cancelled;
        public override string ToString() => Name;
    }
}
