namespace TaskManagement.Core.UserAggregate
{
    public class User : AuditableEntityBase<User, UserId>
    {
        public UserName UserName { get; private set; }
        public UserEmail Email { get; private set; }
        public UserFullName? FullName { get; private set; }
        public UserAvatar? Avatar { get; private set; }
        public bool IsActive { get; private set; }
        private User(UserName userName, UserEmail email, UserFullName? fullName, UserAvatar? avatar)
        {
            UserName = userName;
            Email = email;
            FullName = fullName;
            Avatar = avatar;
            IsActive = false;
        }
        public static User Create(UserName userName, UserEmail email, UserFullName? fullName = null, UserAvatar? avatar = null) => new User(userName, email, fullName, avatar);

        public void UpdateProfile(UserFullName fullName, UserAvatar avatar)
        {
            if (!IsActive)
                throw new InvalidOperationException($"User {UserName} is not active");
            FullName = fullName;
            Avatar = avatar;
        }

        public void Deactive() => IsActive = false;
    }
}
