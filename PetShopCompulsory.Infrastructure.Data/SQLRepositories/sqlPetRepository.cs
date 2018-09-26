using Microsoft.EntityFrameworkCore;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopCompulsory.Infrastructure.Data.SQLRepositories
{
    public class sqlPetRepository : IPetRepository
    {
        readonly PetShopAppContext _ctx;

        public sqlPetRepository(PetShopAppContext ctx)
        {
            _ctx = ctx;
        }

        public Pet AddPetData(Pet pet)
        {
            _ctx.Attach(pet).State = EntityState.Added;
            _ctx.SaveChanges();
            return pet;
        }

        public int Count()
        {
            return _ctx.Pets.Count();
        }

        public int DeletePetData(int id)
        {
            var petToRemove = _ctx.Remove(new Pet { Id = id }).Entity.Id;
            _ctx.SaveChanges();

            return petToRemove;
        }

        public Pet EditPetData(Pet pet)
        {
            _ctx.Attach(pet).State = EntityState.Modified;
            _ctx.Entry(pet).Reference(p => p.PreviousOwner).IsModified = true;
            _ctx.SaveChanges();

            return pet;
        }

        public Pet ReadPetById(int id)
        {
            return _ctx.Pets.Include(p => p.PreviousOwner).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Pet> ReadPets(Filter filter)
        {
           // if (filter == null)
          //  { 

                return _ctx.Pets;
          //  }
          //  return _ctx.Pets.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage).Take(filter.ItemsPrPage);
        }
    }
}
