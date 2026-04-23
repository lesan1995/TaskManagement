namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct IssueTitle
    {
        public string Value { get; init; }
        public const int MaxLength = 100;
        private IssueTitle(string value) => Value = value;
        public static IssueTitle Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Issue Title cannot be empty.");
            if (value.Length > MaxLength)
                throw new ArgumentException($"Issue title cannot be more than {MaxLength} characters");
            return new IssueTitle(value);
        }
        public override string ToString() => Value;
    }
}
