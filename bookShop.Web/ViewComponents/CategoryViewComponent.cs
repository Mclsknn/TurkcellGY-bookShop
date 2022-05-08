using bookShop.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bookShop.Web.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
       
        public  IViewComponentResult Invoke()
        {
            var categories =  _categoryService.GetAllEntities();
            return View(categories);
        }
    }
}
