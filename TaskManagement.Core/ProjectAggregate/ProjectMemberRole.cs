namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct ProjectMemberRole
    {
        public int Value { get; init; }
        public string Name { get; init; }
        private ProjectMemberRole(int value, string name)
        {
            Value = value;
            Name = name;
        }
        public static readonly ProjectMemberRole Manager = new ProjectMemberRole(0, nameof(Manager));
        public static readonly ProjectMemberRole Member = new ProjectMemberRole(1, nameof(Member));
        public static readonly ProjectMemberRole Viewer = new ProjectMemberRole(2, nameof(Viewer));
        public static ProjectMemberRole From(int value) => value switch
        {
            0 => Manager,
            1 => Member,
            2 => Viewer,
            _ => throw new ArgumentException($"Invalid Project Member Role: {value}")
        };
        public override string ToString() => Name;
    }
}
