using AngularProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class DBInitializer
    {
        public static void Initialize(TwoHaxxContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.AddRange(
                new User { FirstName = "", LastName = "", Email = "guest@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = false },
                new User { FirstName = "User", LastName = "User", Email = "user@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = false },
                new User { FirstName = "Moderator", LastName = "Moderator", Email = "moderator@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = false },
                new User { FirstName = "Admin", LastName = "Admin", Email = "admin@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = true }
            );
            context.SaveChanges();
        }
    }
}
