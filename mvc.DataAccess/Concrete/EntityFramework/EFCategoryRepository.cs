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
    public class EFCategoryRepository : ICategoryRepository
    {
        private bookShopDbContext _context;
        private EntityEntry entityEntry;
        bool success;
        public EFCategoryRepository(bookShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Category entity)
        {
            entityEntry = await _context.Categories.AddAsync(entity);
            success = entityEntry.State == EntityState.Added;
            _context.SaveChanges();
            return success;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            entityEntry = _context.Categories.Remove(category);
            success = entityEntry.State == EntityState.Deleted;
            _context.SaveChanges();
            return success;
        }

        public IList<Category> GetAllEntities()
        {
            return _context.Categories.Where(x=> x.IsDeleted == false).ToList();
        }

        public async Task<IList<Category>> GetAllEntitiesAsync()
        {
            var categories = await _context.Categories.Where(c=> c.IsDeleted==false).ToListAsync();
            return categories;
        }

        public async Task<Category> GetEntityByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(p => p.Id == id);
        }

        public async Task<IList<Category>> SearchEntitiesByNameAsync(string name)
        {
            var categories = await _context.Categories.Where(p => p.Name.Contains(name)).ToListAsync();
            return categories;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            category.IsDeleted = true;
            entityEntry = _context.Categories.Update(category);
            success = entityEntry.State == EntityState.Modified;
            _context.SaveChanges();
            return success;
        }

        public bool Update(Category category)
        {
            entityEntry = _context.Categories.Update(category);
            success = entityEntry.State == EntityState.Modified;
            _context.SaveChanges();
            return success;
        }
    }
}
