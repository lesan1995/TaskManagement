namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct AttachmentId
    {
        public int Value { get; init; }
        private AttachmentId(int value) => Value = value;
        public static AttachmentId Create(int value) =>
            value < 0
            ? throw new ArgumentException("AttachmentId must be positive.")
            : new AttachmentId(value);
        public override string ToString() => Value.ToString();
        public static implicit operator int(AttachmentId id) => id.Value;
    }
}
