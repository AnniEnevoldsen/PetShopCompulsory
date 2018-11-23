using System;
using System.Collections.Generic;
using System.Text;
using PetShopCompulsory.Core.Entities;

namespace PetShopCompulsory.Core.ApplicationService
{
    public interface IColorService
    {
        List<PetColor> PetColors();

        PetColor AddPetColor(PetColor petColor);

        int DeletePetColor(int id);

        PetColor EditPetColor(PetColor petColor);

        PetColor FindPetColorById(int id);
    }
}
