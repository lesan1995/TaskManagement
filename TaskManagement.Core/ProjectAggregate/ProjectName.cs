namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct ProjectName
    {
        public const int MaxLength = 100;
        public string Value { get; init; }
        private ProjectName(in string value) => Value = value;
        public static ProjectName Create(in string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Project Name cannot be empty");
            if (value.Length > MaxLength)
                throw new ArgumentException($"Project Name cannot be longer than {MaxLength}");
            return new ProjectName(value);
        }
        public override string ToString() => Value;
    }
}
