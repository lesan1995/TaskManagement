namespace TaskManagement.Core.Interfaces
{
    public interface ICurrentUserService
    {
        UserId UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
        bool IsManager { get; }
    }
}