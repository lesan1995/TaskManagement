using Ardalis.Specification;

namespace TaskManagement.SharedKernel.Common
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
