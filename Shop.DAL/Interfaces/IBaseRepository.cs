using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Domain.Entity;

namespace Shop.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> Get(int id);

        Task<IEnumerable<T>> Select();

        Task Delete(T entity);

        Task Create(T entity);

        Task Update(T entity);
    }
}