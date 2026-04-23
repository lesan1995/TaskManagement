namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct TaskItemId
    {
        public int Value { get; init; }
        private TaskItemId(int value) => Value = value;
        public static TaskItemId Create(int value)
        {
            if (value < 0) throw new ArgumentException("Task Id must be positive.");
            return new TaskItemId(value);
        }
        public override string ToString() => Value.ToString();
        public static implicit operator int(TaskItemId id) => id.Value;
    }
}
