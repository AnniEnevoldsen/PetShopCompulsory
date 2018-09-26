using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PetShopCompulsory.Core.ServiceFolder.implementation
{
    public class PetService : IPetService

    {
        private readonly IPetRepository _petRepository;
        private readonly IOwnerRepository _ownerRepository;
        public PetService(IPetRepository sqlPetRepository, IOwnerRepository sqlOwnerRepository)
        {
            _petRepository = sqlPetRepository;
            _ownerRepository = sqlOwnerRepository;
        }
        public List<Pet> GetPets()
        {
         
            return _petRepository.ReadPets().ToList();
        }

        public List<Pet> GetFilteredPets(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                throw new InvalidDataException("CurrentPage and ItemsPrPage must be above 0");
            }

            if ((filter.CurrentPage -1 * filter.ItemsPrPage) >=_petRepository.Count())
            {

            }
            return _petRepository.ReadPets(filter).ToList();
        }

        public Pet AddPet(Pet pet)
        {
            var petAdd = _petRepository.AddPetData(pet);
            if (pet.PreviousOwner != null)
            {
                petAdd.PreviousOwner = _ownerRepository.ReadOwnerById(pet.PreviousOwner.Id);
            }
        return petAdd;
        }

        public int DeletePet(int id)
        {
            _petRepository.DeletePetData(id);
            return id;
        }

        public Pet EditPet(Pet petToEdit)
        {
            
            return _petRepository.EditPetData(petToEdit);
        }

        public List<Pet> GetThe5CheapestPets()
        {
            return _petRepository.ReadPets().OrderBy(p => p.Price).Take(5).ToList();
        }

        public List<Pet> SortByPrice()
        {
            return _petRepository.ReadPets().OrderBy(p => p.Price).ToList();
        }

        public List<Pet> SearchByType(string search)
        {
            return _petRepository.ReadPets().Where(pet => pet.Type.Contains(search)).ToList();
        }

        public Pet GetAPetInstance()
        {
            return new Pet();
        }

        public Pet FindPetById(int id)
        {
            var pet = _petRepository.ReadPetById(id);

            if (id < 1 && pet == null)
            {
                //pet.PreviousOwner = _ownerRepository.ReadOwnerById(pet.PreviousOwner.OwnerId);
                throw new InvalidDataException("Please type in the id of the pet you wish to change/the pet is null.");
            }
            else
                return pet;
        }
    }
}
