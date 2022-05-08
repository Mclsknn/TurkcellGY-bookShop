using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Dtos.Requests
{
    public class AddPublisherRequest
    {
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Writer> Writers { get; set; }
    }
}
