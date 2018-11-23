using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsory.Core.ApplicationService
{
    public interface IUserService
    {
        List<User> GetAll();
        User Get(int id);
        User Add(User user);
        User Edit(User user);
        int Remove(int id);
    }
}
