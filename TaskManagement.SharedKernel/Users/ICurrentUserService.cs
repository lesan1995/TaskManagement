namespace TaskManagement.SharedKernel.Users
{
    public interface ICurrentUserService
    {
        UserId UserId { get; }
        bool IsManager { get; }
    }
}