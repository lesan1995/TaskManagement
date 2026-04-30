namespace TaskManagement.SharedKernel.User
{
    public record UserInfo
    {
        public UserId UserId { get; init; }
        public string UserName { get; init; } = string.Empty;
        public string? AvatarUrl { get; init; }
    }
}
