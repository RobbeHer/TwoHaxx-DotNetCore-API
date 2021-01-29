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

        public DbSet<User> User { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Talk> Talk { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<UserLikeMessage> UserLikeMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<Talk>().ToTable("Talk");

            modelBuilder.Entity<Talk>()
              .HasOne(v => v.Talker)
              .WithMany(a => a.TalksAsTalker).HasForeignKey(x => x.TalkerID);

            modelBuilder.Entity<Talk>()
              .HasOne(v => v.Moderator)
              .WithMany(a => a.TalksAsModerator).HasForeignKey(x => x.ModeratorID);

            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<UserLikeMessage>().ToTable("UserLikeMessage");
        }
    }
}
