using Microsoft.EntityFrameworkCore;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopCompulsory.Infrastructure.Data.SQLRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PetShopAppContext _ctx;

        public UserRepository(PetShopAppContext ctx)
        {
            _ctx = ctx;
        }

        public User Add(User user)
        {
            _ctx.Add(user);
            _ctx.SaveChanges();
            return user;
        }

        public User Edit(User user)
        {
            _ctx.Entry(user).State = EntityState.Modified;
            _ctx.SaveChanges();
            return user;
        }

        public User Get(int id)
        {
            return _ctx.Users.FirstOrDefault(u => u.Id == id);  
        }

        public IEnumerable<User> GetAll()
        {
            return _ctx.Users.ToList();
        }

        public int Remove(int id)
        {
            var item = _ctx.Users.FirstOrDefault(b => b.Id == id);
            _ctx.Users.Remove(item);
            _ctx.SaveChanges();
            return id;
        }
    }
}
