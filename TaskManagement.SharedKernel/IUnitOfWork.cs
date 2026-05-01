namespace TaskManagement.SharedKernel
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync(CancellationToken ct = default);
        Task CommitAsync(CancellationToken ct = default);
        Task RollBackAsync(CancellationToken ct = default);
    }
}
