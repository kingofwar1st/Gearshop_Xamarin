using GearShopAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GearShopAPI.Data
{
    public class GearContext : DbContext
    {
        public GearContext(DbContextOptions<GearContext> options) : base(options)
        { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<HeadPhone> HeadPhones { get; set; }
        public DbSet<KeyBoard> KeyBoards { get; set; }
        public DbSet<UserInformation> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CPU>().ToTable("CPU");
            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<KeyBoard>().ToTable("KeyBoard");
            modelBuilder.Entity<HeadPhone>().ToTable("HeadPhone");
            modelBuilder.Entity<UserInformation>().ToTable("UserInformation");
        }
    }
}
