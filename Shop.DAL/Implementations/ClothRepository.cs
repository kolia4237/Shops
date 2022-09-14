using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Interfaces;
using Shop.Domain.Entity;

namespace Shop.DAL.Implementations
{
    public class ClothRepository : IClothRepository
    {
        private readonly ApplicationDbContext _db;

        public ClothRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Cloth> Get(int id)
        {
            return await _db.Product.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Cloth>> Select()
        {
            return await _db.Product.ToListAsync();
        }

        public async Task Delete(Cloth entity)
        {
            _db.Product.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(Cloth entity)
        {
            _db.Product.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Cloth entity)
        {
            await _db.SaveChangesAsync();
        }
    }
}