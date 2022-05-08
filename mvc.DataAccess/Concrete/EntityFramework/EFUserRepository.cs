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
    public class EFUserRepository : IUserRepository
    {
        private bookShopDbContext _context;
        private EntityEntry entityEntry;
        public EFUserRepository(bookShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(User entity)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            entity.Password = passwordHash;
            entityEntry = await _context.Users.AddAsync(entity);
            _context.SaveChanges();
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            User category = await _context.Users.FindAsync(id);
            EntityEntry<User> entityEntry = _context.Users.Remove(category);
            return entityEntry.State == EntityState.Deleted;
        }

        public IList<User> GetAllEntities()
        {
            return _context.Users.ToList();
        }

        public async Task<IList<User>> GetAllEntitiesAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetEntityByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(p => p.Id == id);
        }

        public async Task<IList<User>> SearchEntitiesByNameAsync(string name)
        {
            var users = await _context.Users.Where(p => p.UserName.Contains(name)).ToListAsync();
            return users;
        }

        public Task<IList<User>> SearchEntitiesByNameAsync(IList<string> name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            user.IsDeleted = true;
            entityEntry = _context.Users.Update(user);
            return entityEntry.State == EntityState.Modified;
        }

        public bool Update(User entity)
        {
            entityEntry = _context.Users.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
