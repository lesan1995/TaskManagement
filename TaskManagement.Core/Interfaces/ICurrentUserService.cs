namespace TaskManagement.Core.Interfaces
{
    public interface ICurrentUserService
    {
        UserId UserId { get; }
        bool IsManager { get; }
    }
}