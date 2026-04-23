namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct TaskItemIndex
    {
        public int Value { get; init; }
        private TaskItemIndex(int value) => Value = value;
        public static TaskItemIndex Create(int value)
        {
            if (value < 0)
                throw new ArgumentNullException("Task Index cannot be positive");
            return new TaskItemIndex(value);
        }
        public override string ToString() => Value.ToString();
    }
}
