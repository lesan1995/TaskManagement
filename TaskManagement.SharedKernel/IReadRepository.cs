using Ardalis.Specification;

namespace TaskManagement.SharedKernel
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
