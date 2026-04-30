using System.Net.Mail;

namespace TaskManagement.SharedKernel.User
{
    public readonly record struct UserId
    {
        public int Value { get; init; }
        private UserId(int value) => Value = value;
        public static UserId Create(int Value)
        {
            if (Value < 0) throw new ArgumentException("UserId must be positive.");
            return new UserId(Value);
        }
        public static implicit operator string(UserId id) => id.ToString();
        public override string ToString() => Value.ToString();
    }
}
