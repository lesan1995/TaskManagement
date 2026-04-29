namespace TaskManagement.SharedKernel
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string UserName { get; }
        public bool IsAuthenticated { get; }
        public bool IsManager { get; }
    }
}
