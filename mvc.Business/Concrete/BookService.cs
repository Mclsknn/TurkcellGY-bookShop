using AutoMapper;
using bookShop.Business.Abstract;
using bookShop.DataAccess.Abstract;
using bookShop.Dtos.Requests;
using bookShop.Dtos.Responses;
using bookShop.Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Business.Concrete
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository,ICategoryRepository categoryRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(Book entity)
        {
            return await _bookRepository.AddAsync(entity);
        }
        public async Task<bool> AddAsync(AddBookRequest entity)
        {
            var book = _mapper.Map<Book>(entity);
            return await _bookRepository.AddAsync(book);
        }

        public async Task<bool> CreateBook(AddBookRequest book, string categories)
        {
            string s;
            s = categories.Replace(@"""", String.Empty);
            s = s.Replace(@"[", String.Empty);
            s = s.Replace(@"]", String.Empty);
            string[] dizi = s.Split(",");
            Category category1;
            List<int> categories2 = new List<int>();
            List<Category> category = new List<Category>();
            foreach (var item in dizi)
            {
                categories2.Add(Convert.ToInt32(item));
            }
            
            foreach (var item in categories2) 
            {
                category1 = await _categoryRepository.GetEntityByIdAsync(item);
                category.Add(category1);
            }
            book.Categories = category;
            var book2 = _mapper.Map<Book>(book);
            return await _bookRepository.AddAsync(book2);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }

        public async Task<IList<Book>> GetAllEntitiesAsync()
        {
            var books = await _bookRepository.GetAllEntitiesAsync();
            return books;
        }
        public async Task<IList<BookListResponse>> GetAllEntitiesAsyncDto()
        {
            var entities = await _bookRepository.GetAllEntitiesAsync();
            var books = _mapper.Map<IList<BookListResponse>>(entities);
            return books;
        }

        public async Task<Book> GetEntityByIdAsync(int id)
        {
            return await _bookRepository.GetEntityByIdAsync(id);
        }
        public async Task<UpdateBookResponse> GetEntityByIdAsyncDto(int id)
        {
            var book = await _bookRepository.GetEntityByIdAsync(id);
            var entity = _mapper.Map<UpdateBookResponse>(book);
            return entity;
        }

        public async Task<Book> GetEntityByIdAsyncWithoutInclude(int id)
        {
            return await _bookRepository.GetEntityByIdAsyncWithoutInclude(id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
           return await _bookRepository.IsExistsAsync(id);
        }

        public async Task<IList<Book>> SearchEntitiesByNameAsync(string name)
        {
           return await _bookRepository.SearchEntitiesByNameAsync(name);
        }

        public IList<BookListResponse> SearchEntitiesByNameAsync(IList<string> name, IList<string> publisher)
        {
            string s;
            List<int> categories = new List<int>();
            List<int> publishers = new List<int>();
            foreach (var item in name)
            {
                s = item.Replace(@"""", String.Empty);
                s = s.Replace(@"[", String.Empty);
                s = s.Replace(@"]", String.Empty);
                categories.Add(Convert.ToInt32(s));
            }
            foreach (var item in publisher) 
            {
                s = item.Replace(@"""", String.Empty);
                s = s.Replace(@"[", String.Empty);
                s = s.Replace(@"]", String.Empty);
                publishers.Add(Convert.ToInt32(s));
            }
            var books = _mapper.Map<IList<BookListResponse>>(_bookRepository.SearchEntitiesByNameAsync(categories, publishers));
            return books; 
        } 

        public async Task<bool> SoftDeleteAsync(int id)
        {
            return await _bookRepository.SoftDeleteAsync(id);
        }

        public bool Update(Book book)
        {
            return _bookRepository.Update(book);
        }

        public bool UpdateDto(UpdateBookResponse entity)
        {
            var book = _mapper.Map<Book>(entity);
            bool success = _bookRepository.Update(book);
            return success;
        }
    }
}
