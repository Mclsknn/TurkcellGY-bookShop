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
    public interface IWriterService : IGenericService<Writer>
    {
        Task<IList<WriterListResponse>> GetAllEntitiesAsyncDto();
        Task<bool> AddAsync(AddWriterRequest addWriterRequest);
        bool UpdateDto(UpdateWriterResponse entity);
        Task<UpdateWriterResponse> GetEntityByIdAsyncDto(int id);
    }
}
