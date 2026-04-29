using TaskManagement.SharedKernel.User;

namespace TaskManagement.SharedKernel
{
    public interface IUserService
    {
        Task<Dictionary<string, UserInfo>> GetUsersInfoAsync(IEnumerable<string> userIds, CancellationToken ct);
        Task<UserInfo> GetUserAsync(string userId, CancellationToken ct);
    }
}
