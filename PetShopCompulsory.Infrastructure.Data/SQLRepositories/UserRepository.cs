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

        public void Add(User user)
        {
            _ctx.Add(user);
            _ctx.SaveChanges();
        }

        public void Edit(User user)
        {
            _ctx.Entry(user).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public User Get(long id)
        {
            return _ctx.Users.FirstOrDefault(u => u.Id == id);
            
        }

        public IEnumerable<User> GetAll()
        {
            return _ctx.Users.ToList();
        }

        public void Remove(long id)
        {
            var item = _ctx.Users.FirstOrDefault(b => b.Id == id);
            _ctx.Users.Remove(item);
            _ctx.SaveChanges();
        }
    }
}
