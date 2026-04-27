namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct TaskItemIndex
    {
        public int Value { get; init; }
        private TaskItemIndex(int value) => Value = value;
        public static TaskItemIndex Create(int value) =>
            value < 0
            ? throw new ArgumentNullException("Task Index cannot be positive")
            : new TaskItemIndex(value);
        public override string ToString() => Value.ToString();
        public static implicit operator int(TaskItemIndex index) => index.Value;
        public static explicit operator TaskItemIndex(int value) => Create(value);
        public static TaskItemIndex operator +(TaskItemIndex index, int number)
            => Create(index.Value + number);
    }
}
