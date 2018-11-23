using Microsoft.EntityFrameworkCore;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopCompulsory.Infrastructure.Data.SQLRepositories
{
    public class sqlColorRepository : IColorRepository
    {
        readonly PetShopAppContext _ctx;

        public sqlColorRepository(PetShopAppContext ctx)
        {
            _ctx = ctx;
        }

        public PetColor AddPetColorData(PetColor petColor)
        {
            _ctx.Attach(petColor).State = EntityState.Added;
            _ctx.SaveChanges();
            return petColor;
        }

        public int DeletePetColorData(int id)
        {
            var colorToRemove = _ctx.Remove(new PetColor { Id = id }).Entity.Id;
            _ctx.SaveChanges();

            return colorToRemove;
        }

        public PetColor EditPetColorData(PetColor petColor)
        {
            _ctx.Update(petColor);
            _ctx.SaveChanges();
            return petColor;
        }

        public PetColor ReadPetColorById(int id)
        {
            return _ctx.PetColors.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<PetColor> ReadPetColors()
        {
            return _ctx.PetColors;
        }
    }
}
