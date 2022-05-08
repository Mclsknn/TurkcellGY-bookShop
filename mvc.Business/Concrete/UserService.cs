using AutoMapper;
using bookShop.Business.Abstract;
using bookShop.DataAccess.Abstract;
using bookShop.Dtos.Requests;
using bookShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Business.Concrete
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> AddAsync(User entity)
        {
            if (entity.Role == null) 
            {
                entity.Role = "Client";
            }
           bool success = await _userRepository.AddAsync(entity);
            return success;
        }

        public async Task<bool> AddAsync(AddUserRequest addUser)
        {
            if (addUser.Role == null)
            {
                addUser.Role = "Client";
            }
            var user = _mapper.Map<User>(addUser);
            bool success = await _userRepository.AddAsync(user);
            return success;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetAllEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> SearchEntitiesByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> ValidateUser(string userName, string password)
        {
            var users = await _userRepository.GetAllEntitiesAsync();
            User user2 = null;
            foreach (var item in users)
            {
                if (item.UserName == userName)
                {
                    string pass = BCrypt.Net.BCrypt.HashPassword(item.Password);
                    bool success = BCrypt.Net.BCrypt.Verify(item.Password, pass);
                    if (success) {
                        user2 = item;
                        return user2;
                    }
                    
                }
            }
            
            return null;
        }
    }
}
