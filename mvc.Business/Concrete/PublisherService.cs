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
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;
        public PublisherService(IPublisherRepository publisherRepository,IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(Publisher entity)
        {
            return await _publisherRepository.AddAsync(entity);
        }

        public async Task<bool> AddAsync(AddPublisherRequest addPublisherRequest)
        {
            var entity = _mapper.Map<Publisher>(addPublisherRequest);
            return await _publisherRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _publisherRepository.DeleteAsync(id);
        }

        public IList<Publisher> GetAllEntities()
        {
            return _publisherRepository.GetAllEntities();
        }

        public async Task<IList<Publisher>> GetAllEntitiesAsync()
        {
            return await _publisherRepository.GetAllEntitiesAsync();
        }

        public async Task<IList<PublisherListResponse>> GetAllEntitiesAsyncDto()
        {
            var publishers = await _publisherRepository.GetAllEntitiesAsync();
            var ret = _mapper.Map<IList<PublisherListResponse>>(publishers);
            return ret;
        }

        public async Task<Publisher> GetEntityByIdAsync(int id)
        {
            return await _publisherRepository.GetEntityByIdAsync(id);
        }

        public async Task<UpdatePublisherResponse> GetEntityByIdAsyncDto(int id)
        {
            var publisher = await _publisherRepository.GetEntityByIdAsync(id);
            var entity = _mapper.Map<UpdatePublisherResponse>(publisher);
            return entity;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _publisherRepository.IsExistsAsync(id);
        }

        public async Task<IList<Publisher>> SearchEntitiesByNameAsync(string name)
        {
            return await _publisherRepository.SearchEntitiesByNameAsync(name);
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            return await _publisherRepository.SoftDeleteAsync(id);
        }

        public bool Update(Publisher entity)
        {
            return _publisherRepository.Update(entity);
        }

        public bool UpdateDto(UpdatePublisherResponse entity)
        {
            var publisher = _mapper.Map<Publisher>(entity);
            return _publisherRepository.Update(publisher);
        }
    }
}
