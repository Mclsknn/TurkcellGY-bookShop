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

        public async Task<bool> AddAsyncDto(AddUserRequest addUser)
        {
            if (addUser.Role == null)
            {
                addUser.Role = "Client";
            }
            var user = _mapper.Map<User>(addUser);
            bool success = await _userRepository.AddAsync(user);
            return success;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        } 


        public async Task<IList<User>> GetAllEntitiesAsync()
        {
           return await _userRepository.GetAllEntitiesAsync();
        }

        public async Task<IEnumerable<UserListResponse>> GetAllEntitiesAsyncDto()
        {
            var entity = await _userRepository.GetAllEntitiesAsync();
            var users = _mapper.Map<IEnumerable<UserListResponse>>(entity);
            return users;
        }

        public async Task<User> GetEntityByIdAsync(int id)
        {
            return await _userRepository.GetEntityByIdAsync(id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _userRepository.IsExistsAsync(id);
        }

        public Task<IList<User>> SearchEntitiesByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            return await _userRepository.SoftDeleteAsync(id);
        }

        public bool Update(User user)
        {
           return _userRepository.Update(user);
        }

        public async Task<User> ValidateUser(string username, string password)
        {
            var users = await _userRepository.GetAllEntitiesAsync();
            User user2 = null;
            foreach (var item in users)
            {
                if (item.UserName == username || item.UserMail == username)
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
