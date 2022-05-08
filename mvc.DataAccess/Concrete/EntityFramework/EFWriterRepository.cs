using bookShop.DataAccess.Abstract;
using bookShop.DataAccess.Context;
using bookShop.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.DataAccess.Concrete.EntityFramework
{
    public class EFWriterRepository : IWriterRepository
    {
        private bookShopDbContext _context;
        private EntityEntry entityEntry;
        bool success;
        public EFWriterRepository(bookShopDbContext context)
        {
            _context = context;
        }


        public async Task<bool> AddAsync(Writer entity)
        {
            entityEntry = await _context.Writers.AddAsync(entity);
            success = entityEntry.State == EntityState.Added;
            _context.SaveChanges();
            return success;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Writer writers = await _context.Writers.FindAsync(id);
            entityEntry = _context.Writers.Remove(writers);
            success = entityEntry.State == EntityState.Deleted;
            _context.SaveChanges();
            return success;
        }

        public async Task<IList<Writer>> GetAllEntitiesAsync()
        {
            var writers = await _context.Writers.ToListAsync();
            return writers;
        }

        public async Task<Writer> GetEntityByIdAsync(int id)
        {
            var writer = await _context.Writers.FindAsync(id);
            return writer;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Writers.AnyAsync(p => p.Id == id);
        }

        public async Task<IList<Writer>> SearchEntitiesByNameAsync(string name)
        {
            var writers = await _context.Writers.Where(p => p.FullName.Contains(name)).ToListAsync();
            return writers;
        }

        public Task<IList<Writer>> SearchEntitiesByNameAsync(IList<string> name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var writer = await _context.Writers.FindAsync(id);
            writer.IsDeleted = true;
            entityEntry = _context.Writers.Update(writer);
            return entityEntry.State == EntityState.Modified;
        }

        public bool Update(Writer entity)
        {
            entityEntry = _context.Writers.Update(entity);
            success = entityEntry.State == EntityState.Modified;
            _context.SaveChanges();
            return success;
        }
    }
}
