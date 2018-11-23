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

            ctx.PetColors.Add(new PetColor()
            {
               Colors = "Black"
            });

            ctx.PetColors.Add(new PetColor()
            {
                Colors = "White"
            });

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

            string password1 = "pass1";
            byte[] passwordHash1, passwordSalt1;
            CreatePasswordHash(password1, out passwordHash1, out passwordSalt1);
            ctx.Users.Add(new User()
            {
                Username = "username",
                PasswordHash = passwordHash1,
                PasswordSalt = passwordSalt1,
                IsAdmin = false
            });

            string password2 = "adminPass";
            byte[] passwordHash2, passwordSalt2;
            CreatePasswordHash(password2, out passwordHash2, out passwordSalt2);
            ctx.Users.Add(new User()
            {
                Username = "username2",
                PasswordHash = passwordHash2,
                PasswordSalt = passwordSalt2,
                IsAdmin = true
            });
            ctx.SaveChanges();
        }

        // This method computes a hashed and salted password using the HMACSHA512 algorithm.
        // The HMACSHA512 class computes a Hash-based Message Authentication Code (HMAC) using 
        // the SHA512 hash function. When instantiated with the parameterless constructor (as
        // here) a randomly Key is generated. This key is used as a password salt.

        // The computation is performed as shown below:
        //   passwordHash = SHA512(password + Key)

        // A password salt randomizes the password hash so that two identical passwords will
        // have significantly different hash values. This protects against sophisticated attempts
        // to guess passwords, such as a rainbow table attack.
        // The password hash is 512 bits (=64 bytes) long.
        // The password salt is 1024 bits (=128 bytes) long.
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
