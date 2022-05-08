using AutoMapper;
using bookShop.Business.Abstract;
using bookShop.DataAccess.Abstract;
using bookShop.Dtos.Requests;
using bookShop.Dtos.Responses;
using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(Category entity)
        {
            return await _categoryRepository.AddAsync(entity);
        }

        public async Task<bool> AddAsync(AddCategoryRequest entity)
        {
            var category = _mapper.Map<Category>(entity);
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }

        public IList<Category> GetAllEntities()
        {
            return _categoryRepository.GetAllEntities();
        }

        public async Task<IList<Category>> GetAllEntitiesAsync()
        {
            return await _categoryRepository.GetAllEntitiesAsync();
        }

        public async Task<IList<CategoryListResponse>> GetAllEntitiesAsyncDto()
        {
            var categories = await _categoryRepository.GetAllEntitiesAsync();
            var category = _mapper.Map<IList<CategoryListResponse>>(categories);
            return category;
        }

        public async Task<UpdateCategoryResponse> GetEntityByIdAsyncDto(int id)
        {
            var category = await _categoryRepository.GetEntityByIdAsync(id);
            var entity = _mapper.Map<UpdateCategoryResponse>(category);
            return entity;
        }

        public async Task<Category> GetEntityByIdAsync(int id)
        {
            return await _categoryRepository.GetEntityByIdAsync(id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _categoryRepository.IsExistsAsync(id);
        }

        public async Task<IList<Category>> SearchEntitiesByNameAsync(string name)
        {
            return await _categoryRepository.SearchEntitiesByNameAsync(name);
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            return await _categoryRepository.SoftDeleteAsync(id);
        }

        public bool Update(Category entity)
        {
            return  _categoryRepository.Update(entity);
        }

        public bool UpdateDto(UpdateCategoryResponse entity)
        {
            var book = _mapper.Map<Category>(entity);
            return _categoryRepository.Update(book);
        }
    }
}
