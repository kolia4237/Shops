using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Interfaces;
using Shop.Domain.Entity;

namespace Shop.DAL.Implementations
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _db;

        public BasketRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<Basket> Get(int id)
        {
            return await _db.Basket.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<IEnumerable<Basket>> Select()
        {
            return await _db.Basket.ToListAsync();
        }

        public async Task Delete(Basket entity)
        {
            _db.Basket.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(Basket entity)
        {
            _db.Basket.Add(entity);
            await _db.SaveChangesAsync();
        }

        public Task Update(Basket entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Basket>> Select(int id)
        {
            return await _db.Basket.Where(x => x.UserId == id).ToListAsync();
        }
    }
}