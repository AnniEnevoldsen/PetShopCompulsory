using System;
using System.Collections.Generic;
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

        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public void Edit(User user)
        {
            _userRepository.Edit(user);
        }

        public User Get(long id)
        {
            return _userRepository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public void Remove(long id)
        {
            _userRepository.Remove(id);
        }
    }
}
