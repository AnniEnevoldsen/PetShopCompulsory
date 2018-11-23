using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsory.Core.DomainService
{
    public interface IColorRepository
    {
        IEnumerable<PetColor> ReadPetColors();

        PetColor ReadPetColorById(int id);

        PetColor AddPetColorData(PetColor petColor);

        int DeletePetColorData(int id);

        PetColor EditPetColorData(PetColor petColor);
    }
}
