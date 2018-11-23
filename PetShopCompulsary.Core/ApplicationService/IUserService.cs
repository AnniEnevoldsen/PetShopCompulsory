using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsory.Core.ApplicationService
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User Get(long id);
        void Add(User user);
        void Edit(User user);
        void Remove(long id);
    }
}
