using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetApi.Models
{
    public class ShopContextSql : DbContext
    {

        public ShopContextSql(DbContextOptions<ShopContextSql> options) : base(options)
        {        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();

            modelBuilder.Seed();
        }

        public DbSet<User> Users { get; set; }
     
    }
}
