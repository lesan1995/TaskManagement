using Ardalis.Specification;

namespace TaskManagement.SharedKernel.Common
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
