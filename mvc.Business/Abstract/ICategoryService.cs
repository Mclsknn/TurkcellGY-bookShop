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
    public interface ICategoryService : IGenericService<Category>
    {
        IList<Category> GetAllEntities();
        Task<IList<CategoryListResponse>> GetAllEntitiesAsyncDto();
        Task<bool> AddAsync(AddCategoryRequest entity);
        bool UpdateDto(UpdateCategoryResponse entity);
        Task<UpdateCategoryResponse> GetEntityByIdAsyncDto(int id);
    }
}
