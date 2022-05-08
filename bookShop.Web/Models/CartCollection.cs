using bookShop.Dtos.Responses;
using bookShop.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace bookShop.Web.Models
{
    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
    public class CartCollection
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public void Add(CartItem item)
        {
            var finding = CartItems.Find(c => c.Book.Id == item.Book.Id);
            if (finding == null)
            {
                CartItems.Add(item);
            }
            else
            {
                finding.Quantity += item.Quantity;
            }
        }
        public void ClearAll() => CartItems.Clear();
        public double GetTotalPrice() => CartItems.Sum(c => c.Book.Price.Value * (1 - c.Book.Discount.Value) * c.Quantity);
        public void Delete(int id) => CartItems.RemoveAll(c => c.Book.Id == id);
    }
}
