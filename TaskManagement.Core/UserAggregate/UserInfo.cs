namespace TaskManagement.Core.UserAggregate
{
    public record UserInfo
    {
        private UserInfo(UserId userId, string userName, string? avatarUrl)
        {
            UserId = userId;
            UserName = userName;
            AvatarUrl = avatarUrl;
        }
        public static UserInfo Create(UserId userId, string userName = "Unknown User", string? avatarUrl = null)
            => new UserInfo(userId, userName, avatarUrl);
        public UserId UserId { get; init; }
        public string UserName { get; init; }
        public string? AvatarUrl { get; init; }
    }
}
