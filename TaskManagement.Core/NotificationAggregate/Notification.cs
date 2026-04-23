using TaskManagement.Core.User;
using TaskManagement.SharedKernel;

namespace TaskManagement.Core.NotificationAggregate
{
    public class Notification : EntityBase<Notification, NotificationId>, IAggregateRoot
    {
        public NotificationTitle Title { get; private set; }
        public NotificationContent Content { get; private set; }
        public NotificationTime CreatedAt { get; private set; }
        private readonly List<NotificationUser> _users = new();
        public IReadOnlyCollection<NotificationUser> Users => _users.AsReadOnly();
        private Notification(NotificationTitle title, NotificationContent content)
        {
            Title = title;
            Content = content;
            CreatedAt = NotificationTime.Create();
        }
        public static Notification Create(NotificationTitle title, NotificationContent content) => new(title, content);
        public Notification Send(IEnumerable<UserId> userIds)
        {
            foreach (UserId userId in userIds)
                _users.Add(new NotificationUser(Id, userId));
            return this;
        }
    }
}
