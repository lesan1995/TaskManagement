using Ardalis.Specification;

namespace TaskManagement.SharedKernel
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
