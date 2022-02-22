using System.Threading.Tasks;

namespace MicroService.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T Add(T entity);

        void Update(T entity);

        Task<T> GetAsync(int entityId);
    }
}