namespace TaskManagement.SharedKernel.User
{
    public interface IUserService
    {
        Task<Dictionary<UserId, UserInfo>> GetUsersInfoAsync(IEnumerable<UserId> userIds, CancellationToken ct);
        Task<UserInfo> GetUserAsync(UserId userId, CancellationToken ct);
    }
}
