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
    public class WriterService : IWriterService
    {
        private readonly IWriterRepository _writerRepository;
        private readonly IMapper _mapper;
        public WriterService(IWriterRepository writerRepository, IMapper mapper)
        {
            _writerRepository = writerRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(Writer entity)
        {
            return await _writerRepository.AddAsync(entity);
        }

        public async Task<bool> AddAsync(AddWriterRequest addWriterRequest)
        {
            var entity = _mapper.Map<Writer>(addWriterRequest);
            return await _writerRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _writerRepository.DeleteAsync(id);
        }

        public async Task<IList<Writer>> GetAllEntitiesAsync()
        {
            return await _writerRepository.GetAllEntitiesAsync();
        }

        public async Task<IList<WriterListResponse>> GetAllEntitiesAsyncDto()
        {
            var writers = await _writerRepository.GetAllEntitiesAsync();
            var entities = _mapper.Map<IList<WriterListResponse>>(writers);
            return entities;
        }

        public async Task<Writer> GetEntityByIdAsync(int id)
        {
            return await _writerRepository.GetEntityByIdAsync(id);
        }

        public async Task<UpdateWriterResponse> GetEntityByIdAsyncDto(int id)
        {
            var writer = await _writerRepository.GetEntityByIdAsync(id);
            var entity = _mapper.Map<UpdateWriterResponse>(writer);
            return entity;
        }

        public async Task<IList<WriterListResponse>> GetWriterListAsyncDto()
        {
            var writers = await _writerRepository.GetAllEntitiesAsync();
            var ret = _mapper.Map<IList<WriterListResponse>>(writers);
            return ret;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _writerRepository.IsExistsAsync(id);
        }

        public async Task<IList<Writer>> SearchEntitiesByNameAsync(string name)
        {
            return await _writerRepository.SearchEntitiesByNameAsync(name);
        }

        public Task<IList<Writer>> SearchEntitiesByNameAsync(IList<string> name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            return await _writerRepository.SoftDeleteAsync(id);
        }

        public bool Update(Writer entity)
        {
            return _writerRepository.Update(entity);
        }

        public bool UpdateDto(UpdateWriterResponse entity)
        {
            var writer = _mapper.Map<Writer>(entity);
            return _writerRepository.Update(writer);
        }
    }
}
