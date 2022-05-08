using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Dtos.Requests
{
    public class AddCategoryRequest
    {
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
