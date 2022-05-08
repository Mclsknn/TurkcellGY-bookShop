using bookShop.Business.Abstract;
using bookShop.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Business.Concrete
{
    public class GenericService<T,L>where T : class
    {
 
        public GenericService(L l)
        {
        }
        public virtual Task<bool> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAllEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> SearchEntitiesByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
