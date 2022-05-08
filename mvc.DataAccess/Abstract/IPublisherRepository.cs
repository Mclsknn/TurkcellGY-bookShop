using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.DataAccess.Abstract
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        IList<Publisher> GetAllEntities();
    }
}
