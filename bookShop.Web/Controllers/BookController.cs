using bookShop.Business.Abstract;
using bookShop.Dtos.Requests;
using bookShop.Dtos.Responses;
using bookShop.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace bookShop.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IWriterService _writerService;
        private readonly IPublisherService _publisherService;
       
        public BookController(IBookService bookService, ICategoryService categoryService, IWriterService writerService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _writerService = writerService;
            _publisherService = publisherService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string[] category, string[] publishers , int page = 1)
        
        {

            IList<BookListResponse> books;

            if (category.Length != 0 || publishers.Length != 0)
            {
                books = _bookService.SearchEntitiesByNameAsync(category, publishers);
            }
            else 
            {
                books = await _bookService.GetAllEntitiesAsync();
            }
             var paginatedBooks = books.ToPagedList(page, 3);

            //var booksPerPage = 3;
            //var paginatedBooks = books.OrderByDescending(p => p.Id)
            //                                .Skip((page - 1) * booksPerPage)
            //                                .Take(booksPerPage);
            //ViewBag.CurrentPage = page;
            //ViewBag.TotalPages = Math.Ceiling((decimal)books.Count / booksPerPage);

            return View(paginatedBooks);

        }
        public async Task<IActionResult> BookList()
        {
            var books = await _bookService.GetAllEntitiesAsync();
            return View(books);
        }
        public async Task<IActionResult> DetailBook(int id = 11)
        {
            var book = await _bookService.GetEntityByIdAsync(id);
            return View(book);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = new AddBookRequest();
            var categories = _categoryService.GetAllEntities();
            user.Categories = categories;
            ViewBag.SelectedCategory = await GetCategoriesForDropDown();
            ViewBag.SelectedWriter = await GetWritersForDropDown();
            ViewBag.SelectedPublisher = await GetPublishersForDropDown();
            return View(user);
        }

        [HttpPost]
        //public async Task<IActionResult> Create([Bind("Name", "Price", "Discount", "Description", "CategoryId", "ImageUrl")] AddProductRequest request)
        public async Task<IActionResult> Create(AddBookRequest book, string categories)
        {

                var success = await _bookService.CreateBook(book,categories);
                if (success)
                {
                    return Json(true);
                }
                
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.SelectedWriter = await GetWritersForDropDown();
            ViewBag.SelectedPublisher = await GetPublishersForDropDown();
            if (await _bookService.IsExistsAsync(id))
            {
                var book = await _bookService.GetEntityByIdAsyncDto(id);
                return View(book);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBookResponse book)
        {
            if (ModelState.IsValid)
            {
                var success = _bookService.UpdateDto(book);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            ViewBag.Categories = await GetCategoriesForDropDown();
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _bookService.IsExistsAsync(id))
            {
                await _bookService.SoftDeleteAsync(id);
                return Json(true);
            }

            return NotFound();
        }

        public IActionResult Filtred(string[] category, string[] publishers)
        {

            var books1 = _bookService.SearchEntitiesByNameAsync(category, publishers);
            TempData["books2"] = books1;
            return RedirectToAction("Index", new { books1 = books1 });
        }
        public async Task<List<SelectListItem>> GetCategoriesForDropDown()
        {

            List<SelectListItem> selectedCategory = new List<SelectListItem>();
            var items = await _categoryService.GetAllEntitiesAsync();
            foreach (var item in items)
            {
                selectedCategory.Add
                    (
                    new SelectListItem { Text = item.Name, Value = item.Id.ToString() }
                    );
            }
            return selectedCategory;
        }
        public async Task<List<SelectListItem>> GetWritersForDropDown()
        {
            var selectedWriter = new List<SelectListItem>();
            var items = await _writerService.GetAllEntitiesAsync();
            foreach (var item in items)
            {
                selectedWriter.Add
                    (
                    new SelectListItem { Text = item.FullName, Value = item.Id.ToString() }
                    );
            }
            return selectedWriter;
        }
        public async Task<List<SelectListItem>> GetPublishersForDropDown()
        {

            var selectedPublisher = new List<SelectListItem>();
            var items = await _publisherService.GetAllEntitiesAsync();
            foreach (var item in items)
            {
                selectedPublisher.Add
                    (
                    new SelectListItem { Text = item.Name, Value = item.Id.ToString() }
                    );
            }
            return selectedPublisher;
        }

    }
}
