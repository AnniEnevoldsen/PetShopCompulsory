using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;

namespace PetShopCompulsory.Core.ApplicationService.implementation
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _petColorRepository;

        public ColorService(IColorRepository sqlPetColorRepository)
        {
            _petColorRepository = sqlPetColorRepository;
        }

        public PetColor AddPetColor(PetColor petColor)
        {
            return _petColorRepository.AddPetColorData(petColor);
        }

        public int DeletePetColor(int id)
        {
            return _petColorRepository.DeletePetColorData(id);
        }

        public PetColor EditPetColor(PetColor petColor)
        {
            var pet = FindPetColorById(petColor.Id);

            pet.Id = petColor.Id;
            pet.Colors = petColor.Colors;
            return pet;
        }

        public PetColor FindPetColorById(int id)
        {
            return _petColorRepository.ReadPetColorById(id);
        }

        public List<PetColor> PetColors()
        {
            return _petColorRepository.ReadPetColors().ToList();
        }
    }
}
