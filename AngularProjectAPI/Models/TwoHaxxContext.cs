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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Talk>().ToTable("Talks");
            modelBuilder.Entity<Message>().ToTable("Messages");

            modelBuilder.Entity<Talk>()
      .HasOne(v => v.Talker)
      .WithMany(a => a.TalksAsTalker).HasForeignKey(x => x.TalkerID);

            modelBuilder.Entity<Talk>()
     .HasOne(v => v.Moderator)
     .WithMany(a => a.TalksAsModerator).HasForeignKey(x => x.ModeratorID);

        }
    }
}
