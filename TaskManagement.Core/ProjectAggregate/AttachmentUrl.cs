namespace TaskManagement.Core.ProjectAggregate
{
    public readonly record struct AttachmentUrl
    {
        public string Value { get; init; }
        private AttachmentUrl(string value) => Value = value;
        public static AttachmentUrl Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Attachment url cannot be empty.");
            Uri? uri = null;
            if(!Uri.TryCreate(value, UriKind.Relative, out uri))
                throw new ArgumentException("Attachment url is invalid.");
            return new AttachmentUrl(value);
        }
        public override string ToString() => Value;
    }
}
