namespace TaskManagement.SharedKernel.User
{
    public interface ICurrentUserService
    {
        UserId UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
        bool IsManager { get; }
    }
}