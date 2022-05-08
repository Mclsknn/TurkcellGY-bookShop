using bookShop.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
