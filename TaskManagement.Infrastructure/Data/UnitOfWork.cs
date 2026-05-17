using Microsoft.EntityFrameworkCore.Storage;

namespace TaskManagement.Infrastructure.Data
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction; 
        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            if(_transaction != null){
                await _transaction.RollbackAsync();
            };
            _transaction = await dbContext.Database.BeginTransactionAsync(ct);
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            if(_transaction != null)
            {
                await _transaction.CommitAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(ct);
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
