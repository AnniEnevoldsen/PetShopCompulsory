using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;

namespace PetShopCompulsory.Core.ApplicationService.implementation
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnerService(IOwnerRepository sqlOwnerRepository)
        {
            _ownerRepository = sqlOwnerRepository;
        }

        public Owner AddOwner(Owner owner)
        {
            return _ownerRepository.AddOwnerData(owner);
        }

        public int DeleteOwner(int id)
        {
            return _ownerRepository.DeleteOwnerData(id);
        }

        public Owner EditOwner(Owner ownerEdit)
        {
            var owner = FindOwnerById(ownerEdit.Id);
            
            owner.Id = ownerEdit.Id;
            owner.OwnerName = ownerEdit.OwnerName;
            return owner;
        }

        public List<Owner> GetOwners()
        {
            return _ownerRepository.ReadOwners().ToList();
        }

        public Owner FindOwnerById(int id)
        {
            return _ownerRepository.ReadOwnerById(id);
        }
    }
}
