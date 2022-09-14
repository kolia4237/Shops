using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Domain.Entity;

namespace Shop.DAL.Interfaces
{
    public interface IBasketRepository : IBaseRepository<Basket>
    {
        Task<IEnumerable<Basket>> Select(int id);
    }
}