using bookShop.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Business.Abstract
{
    public interface IGenericService<T> where T : class, IEntity, new()
    {
        Task<IList<T>> GetAllEntitiesAsync();
        Task<T> GetEntityByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        bool Update(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
        Task<IList<T>> SearchEntitiesByNameAsync(string name);
        Task<bool> IsExistsAsync(int id);
    }
}
