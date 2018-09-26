using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsory.Core.ApplicationService
{
    public interface IOwnerService
    {
        List<Owner> GetOwners();

        Owner AddOwner(Owner owner);

        int DeleteOwner(int id);

        Owner EditOwner(Owner owner);

        //Owner GetAnOwnerInstance();

        //List<Owner> SearchByType(string search);

        Owner FindOwnerById(int id);
    }
}
