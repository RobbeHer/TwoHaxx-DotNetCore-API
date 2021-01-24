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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");

        }
    }
}
