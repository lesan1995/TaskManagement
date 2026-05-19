namespace TaskManagement.Core.UserAggregate
{
    public readonly record struct UserName
    {
        public const int MinLength = 5;
        public const int MaxLength = 50;
        public string Value { get; init; }
        private UserName(in string value) => Value = value;
        public static UserName Create(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Username cannot be empty");

            if (value.Length < MinLength)
                throw new ArgumentException($"Username cannot be less than {MinLength} characters");

            if (value.Length > MaxLength)
                throw new ArgumentException($"Username cannot be longer than {MaxLength} characters");

            return new UserName(value);
        }

        public override string ToString() => Value;
    }
}
