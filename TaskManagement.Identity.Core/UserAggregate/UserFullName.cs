namespace TaskManagement.Identity.Core.UserAggregate
{
    public readonly record struct UserFullName
    {
        public const int MinLength = 5;
        public const int MaxLength = 200;
        public string Value { get; init; }
        private UserFullName(in string value) => Value = value;
        public static UserFullName Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("FullName cannot be empty");

            if (value.Length < MinLength)
                throw new ArgumentException($"FullName cannot be less than {MinLength} characters");

            if (value.Length > MaxLength)
                throw new ArgumentException($"FullName cannot be longer than {MaxLength} characters");

            return new UserFullName(value);
        }

        public override string ToString() => Value;
    }
}
