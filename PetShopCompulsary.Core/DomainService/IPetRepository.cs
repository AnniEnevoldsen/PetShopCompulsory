using System;
using System.Collections.Generic;
using System.Text;
using PetShopCompulsory.Core.Entities;

namespace PetShopCompulsory.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> ReadPets(Filter filter = null);

        Pet AddPetData(Pet pet);

        int DeletePetData(int id);

        Pet EditPetData(Pet pet);

        Pet ReadPetById(int id);

        int Count();
    }
}
