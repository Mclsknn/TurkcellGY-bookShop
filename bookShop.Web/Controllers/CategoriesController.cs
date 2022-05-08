using bookShop.Business.Abstract;
using bookShop.Dtos.Requests;
using bookShop.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bookShop.Web.Controllers
{
    [Authorize(Roles = "Admin,Editör")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllEntitiesAsyncDto();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryRequest category)
        {

            if (ModelState.IsValid)
            {
                var success = await _categoryService.AddAsyncDto(category);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await _categoryService.IsExistsAsync(id))
            {
                var book = await _categoryService.GetEntityByIdAsyncDto(id);
                return View(book);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var success = _categoryService.Update(category);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _categoryService.IsExistsAsync(id))
            {
                if (await _categoryService.SoftDeleteAsync(id))
                {
                    return Json(true);
                }

            }
            return Json(false);
        }

    }
}
