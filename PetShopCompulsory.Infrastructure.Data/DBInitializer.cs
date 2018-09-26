using PetShopCompulsory.Core.Entities;
using System;

namespace PetShopCompulsory.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedingDB(PetShopAppContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var own1 = ctx.Owners.Add(new Owner()
            {
                OwnerName = "Mr Scrunchy"
            }).Entity;
        

            ctx.Owners.Add(new Owner()
            {
                OwnerName = "Miss Proper"
            });
           

            ctx.Pets.Add(new Pet()
            {
                Name = "Bab",
                Type = "Pig",
                PreviousOwner = own1
            });
           
            ctx.Pets.Add(new Pet()
            {
                Name = "Bibi",
                Type = "Cow"
            });
            ctx.SaveChanges();

            
        }
    }
}
