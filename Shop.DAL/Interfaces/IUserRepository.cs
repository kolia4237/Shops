using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Domain.Entity;

namespace Shop.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByName(string name);
    }
}