using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsory.Core.DomainService
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> ReadOwners();

        Owner ReadOwnerById(int id);

        Owner AddOwnerData(Owner owner);

        int DeleteOwnerData(int id);

        Owner EditOwnerData(Owner owner);
    }
}
