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
                context.Users.AddRange(
                    new User { FirstName = "", LastName = "", Email = "guest@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = false },
                    new User { FirstName = "User", LastName = "User", Email = "user@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = false },
                    new User { FirstName = "Talker", LastName = "Talker", Email = "talker@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = false },
                    new User { FirstName = "Moderator", LastName = "Moderator", Email = "moderator@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = false },
                    new User { FirstName = "Admin", LastName = "Admin", Email = "admin@test.be", Password = "1234", ActiveDirectoryGUID = "", IsAdmin = true }
                );
                context.SaveChanges();
            }

            if (context.Rooms.Any()) 
            {
                context.Rooms.AddRange(
                    new Room { Name = "Room 1", Description = "Description room 1", StartDate = new DateTime(), EndDate = new DateTime() }
                );
                context.SaveChanges();
            }

            if (context.Talks.Any())
            {
                context.Talks.AddRange(
                    new Talk { Name = "Talk 1", Description = "Description talk 1", StartDate = new DateTime(), EndDate = new DateTime(), TalkerID = 3, RoomID = 1 }
                );
                context.SaveChanges();
            }
        }
    }
}
