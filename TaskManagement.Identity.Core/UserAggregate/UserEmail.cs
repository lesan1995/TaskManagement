using System.Text.RegularExpressions;

namespace TaskManagement.Identity.Core.UserAggregate
{
    public readonly record struct UserEmail
    {
        public const int MaxLength = 100;
        public string Value { get; init; }
        private UserEmail(string value) => Value = value;
        public static UserEmail Create(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Email cannot be empty");
            if (value.Length > MaxLength)
                throw new ArgumentException($"Email cannot be longer than {MaxLength} characters");
            string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            if (!Regex.IsMatch(value, emailPattern))
                throw new ArgumentException($"Email is invalid");
            return new UserEmail(value);
        }
        public override string ToString() => Value;
    }
}
