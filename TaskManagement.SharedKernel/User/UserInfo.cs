namespace TaskManagement.SharedKernel.User
{
    public record UserInfo
    {
        public string UserId { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string? AvatarUrl { get; init; }
    }
}
