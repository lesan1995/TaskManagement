namespace TaskManagement.Core.UserAggregate
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
        public override string ToString() => Value.ToString();
    }
}
