namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct ProjectId
    {
        public int Value { get; init; }
        private ProjectId(int value) => Value = value;
        public static ProjectId Create(int value)
        {
            if (value < 0)
                throw new ArgumentException("ProjectId must be positive.");
            return new ProjectId(value);
        }
        public override string ToString() => Value.ToString();
        public static implicit operator int(ProjectId id) => id.Value;
    }
}
