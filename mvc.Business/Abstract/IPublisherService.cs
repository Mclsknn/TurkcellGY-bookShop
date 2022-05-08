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
    public interface IPublisherService : IGenericService<Publisher>
    {
        IList<Publisher> GetAllEntities();
        Task<IList<PublisherListResponse>> GetAllEntitiesAsyncDto();
        Task<bool> AddAsync(AddPublisherRequest addPublisherRequest);
        bool UpdateDto(UpdatePublisherResponse entity);
        Task<UpdatePublisherResponse> GetEntityByIdAsyncDto(int id);
    }
}
