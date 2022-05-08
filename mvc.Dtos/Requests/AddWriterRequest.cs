using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Dtos.Requests
{
    public class AddWriterRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Publisher> Publishers { get; set; }
    }
}
