namespace TaskManagement.Core.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string UserName { get; }
        public bool IsAuthenticated { get; }
    }
}
