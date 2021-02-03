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
                return;
            }

            context.Users.AddRange(
                    new User { FirstName = "User", LastName = "User", Email = "user@test.be", Password = "1234", IsAdmin = false, IsGuest = false },
                    new User { FirstName = "Talker", LastName = "Talker", Email = "talker@test.be", Password = "1234", IsAdmin = false, IsGuest = false },
                    new User { FirstName = "Moderator", LastName = "Moderator", Email = "moderator@test.be", Password = "1234", IsAdmin = false, IsGuest = false },
                    new User { FirstName = "Admin", LastName = "Admin", Email = "admin@test.be", Password = "1234", IsAdmin = true, IsGuest = false },
                    new User { FirstName = "Guest1", LastName = "Guest1", Email = "guest1@test.be", Password = "1234", IsAdmin = false, IsGuest = true },
                    new User { FirstName = "Guest2", LastName = "Guest2", Email = "guest2@test.be", Password = "1234", IsAdmin = false, IsGuest = true },
                    new User { FirstName = "Guest3", LastName = "Guest3", Email = "guest3@test.be", Password = "1234", IsAdmin = false, IsGuest = true },
                    new User { FirstName = "Guest4", LastName = "Guest4", Email = "guest3@test.be", Password = "1234", IsAdmin = false, IsGuest = true },
                    new User { FirstName = "Guest5", LastName = "Guest5", Email = "guest5@test.be", Password = "1234", IsAdmin = false, IsGuest = true }



                );
            context.SaveChanges();



            context.Rooms.AddRange(
                    new Room { Name = "Room 1", Description = "Description room 1", StartDate = new DateTime(), EndDate = new DateTime() },
                    new Room { Name = "Room 2", Description = "Description room 2", StartDate = new DateTime(), EndDate = new DateTime() }
                );
            context.SaveChanges();



            context.Talks.AddRange(
                                        new Talk { Name = "Talk 1", Description = "Description talk 1", Code = "1234", StartDate = new DateTime(), EndDate = new DateTime(), TalkerID = 3, ModeratorID = 4, RoomID = 1 },
                    new Talk { Name = "Talk 2", Description = "Description talk 2", Code = "1234", StartDate = new DateTime(), EndDate = new DateTime(), TalkerID = 3, ModeratorID = 4, RoomID = 1 },
                    new Talk { Name = "Talk 3", Description = "Description talk 3", Code = "1234", StartDate = new DateTime(), EndDate = new DateTime(), TalkerID = 3, ModeratorID = 4, RoomID = 1 },
                    new Talk { Name = "Talk 4", Description = "Description talk 4", Code = "1234", StartDate = new DateTime(), EndDate = new DateTime(), TalkerID = 3, ModeratorID = 4, RoomID = 1 },
                    new Talk { Name = "Talk 1", Description = "Description talk 1", Code = "1234", StartDate = new DateTime(), EndDate = new DateTime(), TalkerID = 3, ModeratorID = 4, RoomID = 2 },
                    new Talk { Name = "Talk 2", Description = "Description talk 2", Code = "1234", StartDate = new DateTime(), EndDate = new DateTime(), TalkerID = 3, ModeratorID = 4, RoomID = 2 }
                );
            ;
            context.SaveChanges();



            context.Messages.AddRange(
                    new Message { Content = "Message 1", TimeStamp = new DateTime(), UserID = 1, RoomID = 1 },
                    new Message { Content = "Message 2", TimeStamp = new DateTime(), UserID = 2, RoomID = 1 },
                    new Message { Content = "Message 3", TimeStamp = new DateTime(), UserID = 3, RoomID = 1 },
                    new Message { Content = "Message 4", TimeStamp = new DateTime(), UserID = 4, RoomID = 1 },
                    new Message { Content = "Message 5", TimeStamp = new DateTime(), UserID = 5, RoomID = 1 }
                );
            context.SaveChanges();

            context.Feedbacks.AddRange(
                    new Feedback { Score = 5, Content = "Good talk.", UserID = 1, TalkID = 1 },
                    new Feedback { Score = 5, Content = "Good talk.", UserID = 2, TalkID = 1 }
                );
            context.SaveChanges();

            context.Polls.AddRange(
                    new Poll { Name = "Poll 1", Question = "My question", RoomID = 1 },
                    new Poll { Name = "Poll 2", Question = "My second question", RoomID = 1 }
                );
            context.SaveChanges();

            context.PollOptions.AddRange(
                    new PollOption { Content = "Answer 1", PollID = 1 },
                    new PollOption { Content = "Answer 2", PollID = 1 },
                    new PollOption { Content = "Answer 1 of poll 2", PollID = 2 },
                    new PollOption { Content = "Answer 2 of poll 2", PollID = 2 }
                );
            context.SaveChanges();
        }
    }
}
