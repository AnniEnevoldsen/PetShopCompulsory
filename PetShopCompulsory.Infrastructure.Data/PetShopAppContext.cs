using Microsoft.EntityFrameworkCore;
using PetShopCompulsory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsory.Infrastructure.Data
{
    public class PetShopAppContext : DbContext
    {
        public PetShopAppContext(DbContextOptions<PetShopAppContext> opt): base(opt){}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Owner>()
        //  .HasOne(o => o).WithMany(p => p)
        //  .OnDelete(DeleteBehavior.SetNull);
        //}

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}

