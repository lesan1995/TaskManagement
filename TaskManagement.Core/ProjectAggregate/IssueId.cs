namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct IssueId
    {
        public int Value { get; init;  }
        private IssueId(int value) => Value = value;
        public static IssueId Create(int value)
        {
            if (value < 0) throw new ArgumentException("Issue Id cannot be positive.");
            return new IssueId(value);
        }
        public override string ToString() => Value.ToString();
        public static implicit operator int(IssueId id) => id.Value;
    }
}
