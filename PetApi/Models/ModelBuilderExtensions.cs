using Microsoft.EntityFrameworkCore;

namespace PetApi.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name= "Adam", Description="Admin", Email = "adam@example.com", Address="Dhaka", Mobile="0158888", PostCode="1215", ImageUrl = "assets/images/adam.png" },
                new User { Id = 2, Name = "BarBara", Description = "SuperAdmin", Email = "barbara@example.com", Address = "Dhaka", Mobile = "0168888", PostCode = "1215", ImageUrl = "assets/images/barbara.png" });
        }
    }

}
