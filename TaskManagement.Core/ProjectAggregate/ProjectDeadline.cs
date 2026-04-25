namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct ProjectDeadline
    {
        public DateTime Value { get; init; }
        private ProjectDeadline(DateTime value) => Value = value.Date;
        public static ProjectDeadline Create(DateTime value)
        {
            var dateOnly = value.Date;
            if (dateOnly < DateTime.UtcNow.Date)
                throw new ArgumentException("Project Deadline cannot be in the past.");
            return new ProjectDeadline(dateOnly);
        }
        public static ProjectDeadline Default() => Create(DateTime.UtcNow.AddDays(1));
        public bool IsExpired() => Value < DateTime.UtcNow.Date;
        public int DaysLeft() => (Value - DateTime.UtcNow.Date).Days;
        public override string ToString() => Value.ToString("dd/MM/yyyy");
    }
}
