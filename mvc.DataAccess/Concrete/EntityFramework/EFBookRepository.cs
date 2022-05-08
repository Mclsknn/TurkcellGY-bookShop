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
    public class EFBookRepository : IBookRepository
    {
        private bookShopDbContext _context;
        private EntityEntry entityEntry;
        bool success = false;
        public EFBookRepository(bookShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Book entity)
        {
            entityEntry = await _context.Books.AddAsync(entity);
            success = entityEntry.State == EntityState.Added;
            _context.SaveChanges();
            return success;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Book book = await _context.Books.FindAsync(id);
            entityEntry = _context.Books.Remove(book);
            _context.SaveChanges();
            success = entityEntry.State == EntityState.Deleted;

            return success;
        }

        public async Task<IList<Book>> GetAllEntitiesAsync()
        {
            var books = await _context.Books.Where(x => x.IsDeleted == false)
                                            .Include(x => x.Publisher)
                                            .Include(y => y.Writer)
                                            .Include(z => z.Categories)
                                            .ToListAsync();
            return books;
        }

        public async Task<Book> GetEntityByIdAsync(int id)
        {
            var entities = await _context.Books.Include(x => x.Writer).Include(y => y.Publisher).Include(c => c.Categories).ToListAsync();
            var book = entities.Where(x => x.Id == id).FirstOrDefault();
            return book;
        }

        public async Task<Book> GetEntityByIdAsyncWithoutInclude(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(p => p.Id == id);
        }

        public IList<Book> SearchEntitiesByNameAsync(IList<int> category, IList<int> publisher)
        {
            List<Book> books = new List<Book>();
            List<Book> books3 = new List<Book>();
            List<Category> categories = new List<Category>();
            var c = _context.Categories.ToList();

            foreach (var item in category)
            {
                var k = c.Where(x => x.Id == item).FirstOrDefault();
                categories.Add(k);
            }

            foreach (var item in publisher)
            {
                var book2 = _context.Books.Where(x => x.PublisherId == item).Include(c => c.Categories);

                foreach (var item2 in book2)
                {
                    books.Add(item2);
                }
            }

            foreach (var item in categories)
            {

                var h = books.Where(x => x.Categories.Contains(item)).ToList();
                foreach (var item2 in h)
                {
                    books3.Add(item2);
                }
            }

            return books3;
        }

        public async Task<IList<Book>> SearchEntitiesByNameAsync(string name)
        {
            var books = await _context.Books.Where(p => p.Name.Contains(name)).ToListAsync();
            return books;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            book.IsDeleted = true;
            entityEntry = _context.Books.Update(book);
            success = entityEntry.State == EntityState.Modified;
            _context.SaveChanges();
            return success;
        }

        public bool Update(Book entity)
        {
            entityEntry = _context.Books.Update(entity);
            success = entityEntry.State == EntityState.Modified;
            _context.SaveChanges();
            return success;
        }
    }
}
