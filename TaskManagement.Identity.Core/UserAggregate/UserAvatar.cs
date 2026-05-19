namespace TaskManagement.Identity.Core.UserAggregate
{
    public readonly record struct UserAvatar
    {
        public string Value { get; init; }
        private UserAvatar(string value) => Value = value;
        public static UserAvatar Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("User avatar url cannot be empty.");
            Uri? uri = null;
            if (!Uri.TryCreate(value, UriKind.Relative, out uri))
                throw new ArgumentException("User avatar url is invalid.");
            return new UserAvatar(value);
        }
        public override string ToString() => Value;
    }
}
