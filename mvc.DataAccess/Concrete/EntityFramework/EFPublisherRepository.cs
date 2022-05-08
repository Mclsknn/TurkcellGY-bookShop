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
    public class EFPublisherRepository : IPublisherRepository
    {
        private bookShopDbContext _context;
        private EntityEntry entityEntry;
        bool success;
        public EFPublisherRepository(bookShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Publisher entity)
        {
            entityEntry = await _context.Publishers.AddAsync(entity);
            success = entityEntry.State == EntityState.Added;
            _context.SaveChanges();
            return success;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Publisher publisher = await _context.Publishers.FindAsync(id);
            entityEntry = _context.Publishers.Remove(publisher);
            success = entityEntry.State == EntityState.Deleted;
            _context.SaveChanges();
            return success;
        }

        public IList<Publisher> GetAllEntities()
        {
            return _context.Publishers.Where(x=> x.IsDeleted == false).ToList();
        }

        public async Task<IList<Publisher>> GetAllEntitiesAsync()
        {
            var publishers = await _context.Publishers.Where(x=> x.IsDeleted == false).ToListAsync();
            return publishers;
        }

        public async Task<Publisher> GetEntityByIdAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            return publisher;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Publishers.AnyAsync(p => p.Id == id);
        }

        public async Task<IList<Publisher>> SearchEntitiesByNameAsync(string name)
        {
            var publishers = await _context.Publishers.Where(p => p.Name.Contains(name)).ToListAsync();
            return publishers;
        }
        public async Task<bool> SoftDeleteAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            publisher.IsDeleted = true;
            entityEntry = _context.Publishers.Update(publisher);
            success = entityEntry.State == EntityState.Modified;    
            _context.SaveChanges();
            return success;
        }

        public bool Update(Publisher publisher)
        {
            entityEntry = _context.Publishers.Update(publisher);
            success = entityEntry.State == EntityState.Modified;
            _context.SaveChanges();
            return success;
        }
    }
}
