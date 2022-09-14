using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Interfaces;
using Shop.Domain.Entity;

namespace Shop.DAL.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> Get(int id)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> Select()
        {
            return await _db.User.ToListAsync();
        }

        public async Task Delete(User entity)
        {
            _db.User.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(User entity)
        {
            await _db.User.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetByName(string name)
        {
            /*return await _db.User.Include(x => x.Basket).Include(x => x.Basket.Cloths).FirstOrDefaultAsync(x => x.Name == name);*/
            return await _db.User.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task Update(User entity)
        {
            _db.User.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}