using bookShop.Business.Abstract;
using bookShop.Dtos.Requests;
using bookShop.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bookShop.Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        public async Task<IActionResult> Index()
        {
            var publisher = await _publisherService.GetAllEntitiesAsyncDto();
            return View(publisher);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddPublisherRequest publisher)
        {

            if (ModelState.IsValid)
            {
                var success = await _publisherService.AddAsync(publisher);
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
            if (await _publisherService.IsExistsAsync(id))
            {
                var book = await _publisherService.GetEntityByIdAsyncDto(id);
                return View(book);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                var success = _publisherService.Update(publisher);
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
            if (await _publisherService.IsExistsAsync(id))
            {
                await _publisherService.SoftDeleteAsync(id);
                return Json(true);
            }

            return NotFound();
        }

    }
}
