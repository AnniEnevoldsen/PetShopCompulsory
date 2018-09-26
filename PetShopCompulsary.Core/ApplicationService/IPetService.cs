using System;
using System.Collections.Generic;
using System.Text;
using PetShopCompulsory.Core.Entities;

namespace PetShopCompulsory.Core.ServiceFolder
{
    public interface IPetService
    {
        List<Pet> GetPets();

        List<Pet> GetFilteredPets(Filter filter);

        Pet AddPet(Pet pet);

        int DeletePet(int id);

        Pet EditPet(Pet pet);

        Pet GetAPetInstance();

        List<Pet> GetThe5CheapestPets();

        List<Pet> SortByPrice();

        List<Pet> SearchByType(string search);

        Pet FindPetById(int id);

        
    }
}
