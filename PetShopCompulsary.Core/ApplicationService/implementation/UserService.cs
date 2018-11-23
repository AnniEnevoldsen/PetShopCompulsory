using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;

namespace PetShopCompulsory.Core.ApplicationService.implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
          
        }

        public User Add(User user)
        {
            return _userRepository.Add(user);
        }

        public User Edit(User user)
        {
            return _userRepository.Edit(user);
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public int Remove(int id)
        {
            return _userRepository.Remove(id);
        }
    }
}
