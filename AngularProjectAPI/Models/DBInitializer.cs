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

            if (context.User.Any())
            {
                return;
            }

            context.User.AddRange(
                    new User { UserID = 1, FirstName = "", LastName = "", Email = "guest@test.be", Password = "1234", IsAdmin = false, IsGuest = true },
                    new User { UserID = 2, FirstName = "User", LastName = "User", Email = "user@test.be", Password = "1234", IsAdmin = false, IsGuest = false },
                    new User { UserID = 3, FirstName = "Talker", LastName = "Talker", Email = "talker@test.be", Password = "1234", IsAdmin = false, IsGuest = false },
                    new User { UserID = 4, FirstName = "Moderator", LastName = "Moderator", Email = "moderator@test.be", Password = "1234", IsAdmin = false, IsGuest = false },
                    new User { UserID = 5, FirstName = "Admin", LastName = "Admin", Email = "admin@test.be", Password = "1234", IsAdmin = true, IsGuest = false }
                );
            context.SaveChanges();

            context.Room.AddRange(
                    new Room { Name = "Room 1", Description = "Description room 1", StartDate = new DateTime(), EndDate = new DateTime() }
                );
            context.SaveChanges();

            context.Talk.AddRange(
                    new Talk { Name = "Talk 1", Description = "Description talk 1", StartDate = new DateTime(), EndDate = new DateTime(), TalkerID = 3, ModeratorID = 4, RoomID = 1 }
                );
            context.SaveChanges();

            context.Message.AddRange(
                    new Message { Content = "Message 1", TimeStamp = new DateTime(), UserID = 1, RoomID = 1 },
                    new Message { Content = "Message 2", TimeStamp = new DateTime(), UserID = 2, RoomID = 1 },
                    new Message { Content = "Message 3", TimeStamp = new DateTime(), UserID = 3, RoomID = 1 },
                    new Message { Content = "Message 4", TimeStamp = new DateTime(), UserID = 4, RoomID = 1 },
                    new Message { Content = "Message 5", TimeStamp = new DateTime(), UserID = 5, RoomID = 1 }
                );
            context.SaveChanges();
        }
    }
}
