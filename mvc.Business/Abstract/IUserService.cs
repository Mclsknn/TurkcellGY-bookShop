using bookShop.Dtos.Requests;
using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Business.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> ValidateUser(string userName, string password);
        Task<bool> AddAsync(AddUserRequest addUser);
    }
}
