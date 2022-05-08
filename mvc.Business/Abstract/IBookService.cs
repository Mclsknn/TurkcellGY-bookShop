using bookShop.Dtos.Requests;
using bookShop.Dtos.Responses;
using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Business.Abstract
{
    public interface IBookService : IGenericService<Book>
    {
        Task<IList<BookListResponse>> SearchEntitiesByNameAsync(IList<string> name);
        IList<BookListResponse> SearchEntitiesByNameAsync(IList<string> name, IList<string> publisher);
        Task<IList<BookListResponse>> GetAllEntitiesAsync();

        Task<UpdateBookResponse> GetEntityByIdAsyncDto(int id);
         Task<bool> AddAsync(AddBookRequest entity);
        bool UpdateDto(UpdateBookResponse entity);
        Task<bool> CreateBook(AddBookRequest book, string categories);
    }
}
