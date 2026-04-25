namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct IssueContent
    {
        public string Value { get; init; }
        public const int MaxLength = 100;
        private IssueContent(string value) => Value = value;
        public static IssueContent Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Issue Title cannot be empty.");
            if (value.Length > MaxLength)
                throw new ArgumentException($"Issue title cannot be more than {MaxLength} characters");
            return new IssueContent(value);
        }
        public override string ToString() => Value;
    }
}
