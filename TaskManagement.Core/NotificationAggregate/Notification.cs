namespace TaskManagement.Core.NotificationAggregate
{
    public class Notification : EntityBase<Notification, NotificationId>, IAggregateRoot
    {
        public NotificationContent Content { get; private set; }
        public NotificationTime CreatedAt { get; private set; }
        private readonly List<NotificationUser> _users = new();
        public IReadOnlyCollection<NotificationUser> Users => _users.AsReadOnly();
        private Notification(NotificationContent content)
        {
            Content = content;
            CreatedAt = NotificationTime.Create();
        }
        public static Notification Create(NotificationContent content) => new(content);
        public Notification Send(IEnumerable<UserId> userIds)
        {
            foreach (UserId userId in userIds)
                _users.Add(new NotificationUser(Id, userId));
            return this;
        }
        public void Read(UserId userId)
        {
            var user = _users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                throw new InvalidOperationException($"User {userId} not receive notification {Id}");
            if (user.IsRead) return;
            user.Read();
        }
    }
}
