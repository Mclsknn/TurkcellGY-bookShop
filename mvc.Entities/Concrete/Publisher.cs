using bookShop.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Entities.Concrete
{
    public class Publisher : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Writer> Writers { get; set; }
    }
}
