using Microsoft.EntityFrameworkCore;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopCompulsory.Infrastructure.Data.SQLRepositories
{
    public class sqlOwnerRepository : IOwnerRepository
    {
        readonly PetShopAppContext _ctx;

        public sqlOwnerRepository(PetShopAppContext ctx)
        {
            _ctx = ctx;
        }

        public Owner AddOwnerData(Owner owner)
        {
            //var newOwner = _ctx.Add(owner).Entity;
            //_ctx.SaveChanges();
            //return newOwner;

            _ctx.Attach(owner).State = EntityState.Added;
            _ctx.SaveChanges();
            return owner;
        }

        public int DeleteOwnerData(int id)
        {
            var ownerToRemove = _ctx.Remove(new Owner { Id = id }).Entity.Id;
            _ctx.SaveChanges();

            return ownerToRemove;
        }

        public Owner EditOwnerData(Owner owner)
        {
            _ctx.Update(owner);
            _ctx.SaveChanges();
            return owner;
        }

        public Owner ReadOwnerById(int id)
        {
            return _ctx.Owners.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return _ctx.Owners;
        }
    }
}
