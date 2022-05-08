using bookShop.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Entities.Concrete
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPages { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DateofPublish { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string CoverType { get; set; }
        public double Width { get; set; }
        public double Heigth { get; set; }
        public bool IsDeleted { get; set; }
        public Writer Writer { get; set; }
        public int? WriterId { get; set; }
        public Publisher Publisher { get; set; }
        public int? PublisherId { get; set; }
        public ICollection<Category> Categories{ get; set; }
    }
}
