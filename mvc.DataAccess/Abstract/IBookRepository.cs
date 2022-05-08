using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.DataAccess.Abstract
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IList<Book> SearchEntitiesByNameAsync(IList<int> name, IList<int> publisher);
        Task<Book> GetEntityByIdAsyncWithoutInclude(int id);

    }
}
