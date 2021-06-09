using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularProjectAPI.Models
{
    public class TwoHaxxContext : DbContext
    {
        public TwoHaxxContext(DbContextOptions<TwoHaxxContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserLikeMessage> UserLikeMessage { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<VoteUser> VoteUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Talk>().ToTable("Talks");

            modelBuilder.Entity<Talk>()
              .HasOne(v => v.Talker)
              .WithMany(a => a.TalksAsTalker).HasForeignKey(x => x.TalkerID);

            modelBuilder.Entity<Talk>()
              .HasOne(v => v.Moderator)
              .WithMany(a => a.TalksAsModerator).HasForeignKey(x => x.ModeratorID);

            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<UserLikeMessage>().ToTable("UserLikeMessages");
            modelBuilder.Entity<Feedback>().ToTable("Feedbacks");
            modelBuilder.Entity<Poll>().ToTable("Polls");
            modelBuilder.Entity<PollOption>().ToTable("PollOptions");
            modelBuilder.Entity<VoteUser>().ToTable("VoteUsers");
        }
    }
}
