using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

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
    }
}
