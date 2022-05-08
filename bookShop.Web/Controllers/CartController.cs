using bookShop.Business.Abstract;
using bookShop.Entities.Concrete;
using bookShop.Web.Extensions;
using bookShop.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace bookShop.Web.Controllers
{
    //public class CartController : Controller
    //{
    //    private readonly IBookService _bookService;

    //    public CartController(IBookService bookService)
    //    {
    //        _bookService = bookService;
    //    }
    //    public IActionResult Index()
    //    {
    //        var cartCollection = GetCollectionFromSession();
    //        return View(cartCollection);
    //    }
    //    public async Task<IActionResult> Add(int id)
    //    {

    //        //if (User.Identity.IsAuthenticated)
    //        //{
    //            if (await _bookService.IsExistsAsync(id))
    //            {
    //                CartCollection cartCollection = GetCollectionFromSession();
    //                var book = await _bookService.GetEntityByIdAsync(id);
    //                cartCollection.Add(new CartItem { Book = book, Quantity = 1 });
    //                SaveToSession(cartCollection);
    //                return Json(new JsonReturnObject { text = $"{book.Name }Sepete eklendi" , success = true});
    //            }
    //       // }
    //       /* else 
    //        {
    //            return Json(new JsonReturnObject { text = "/Users/Login", success = false }) ;
    //        }*/

    //        return NotFound();
    //    }

    //    private void SaveToSession(CartCollection cartCollection)
    //    {
    //        HttpContext.Session.SetString("mycart", JsonConvert.SerializeObject(cartCollection));
    //    }

    //    private CartCollection GetCollectionFromSession()
    //    {
    //        CartCollection collection = HttpContext.Session.GetJson<CartCollection>("mycart") ?? new CartCollection();
    //        return collection;
    //    }
    //}


    public class CartController : Controller
    {
        private readonly IBookService _bookService;
        public CartController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public IActionResult Index()
        {
            var cartCollection = getCollectionFromSession();
            return View(cartCollection);
        }
        public async Task<IActionResult> Add(int id)
        {
            if (await _bookService.IsExistsAsync(id))
            {
                CartCollection cartCollection = getCollectionFromSession();
                var book = await _bookService.GetEntityByIdAsyncWithoutInclude(id);
                cartCollection.Add(new CartItem { Book = book, Quantity = 1 });
                saveToSession(cartCollection);
                return Json(new JsonReturnObject { text = $"{book.Name }Sepete eklendi", success = true });
            }
            return Json(new JsonReturnObject { text = "Ürün ekleme başarısız", success = false });
        }

        private void saveToSession(CartCollection cartCollection)
        {
            HttpContext.Session.SetJson("sepetim", cartCollection);
        }

        private CartCollection getCollectionFromSession()
        {
            //CartCollection cartCollection = null;
            //if (HttpContext.Session.Get("sepetim") == null)
            //{
            //    cartCollection = new CartCollection();
            //}
            //else 
            //{
            //    var carCollectionJson = HttpContext.Session.GetString("sepetim");
            //    cartCollection = JsonConvert.DeserializeObject<CartCollection>(carCollectionJson);

            CartCollection collection = HttpContext.Session.GetJson<CartCollection>("sepetim") ?? new CartCollection();

            //}
            return collection;
        }
    }
}
