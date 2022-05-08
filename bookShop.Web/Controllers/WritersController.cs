using bookShop.Business.Abstract;
using bookShop.Dtos.Requests;
using bookShop.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bookShop.Web.Controllers
{
    [Authorize(Roles="Admin,Editör")]
    public class WritersController : Controller
    {
        private readonly IWriterService _writerService;
        public WritersController(IWriterService writerService)
        {
            _writerService = writerService;
        }
        public async Task<IActionResult> Index()
        {
            var writers = await _writerService.GetAllEntitiesAsyncDto();
            return View(writers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddWriterRequest writer)
        {

            if (ModelState.IsValid)
            {
                var success = await _writerService.AddAsync(writer);
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
            if (await _writerService.IsExistsAsync(id))
            {
                var book = await _writerService.GetEntityByIdAsyncDto(id);
                return View(book);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Writer writer)
        {
            if (ModelState.IsValid)
            {
                var success = _writerService.Update(writer);
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
            if (await _writerService.IsExistsAsync(id))
            {
                await _writerService.SoftDeleteAsync(id);
                return Json(true);
            }

            return Json(false);
        }

    }
}
