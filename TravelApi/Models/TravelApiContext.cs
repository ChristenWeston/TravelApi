using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace TravelApi.Models
{
    public class TravelApiContext : DbContext
    {
        public TravelApiContext(DbContextOptions<TravelApiContext> options)
            : base(options) 
        {
        }
        public DbSet<Review> Reviews { get; set; }
// Protected override since we only want this method to be accessible to the class itself and we want to override the default method
//Since the method doesn't return anything we specify void as the return value
        protected override void OnModelCreating(ModelBuilder builder)
        {
          builder.Entity<Review>()
            .HasData(
              new Review { ReviewId = 1, Rating = 5, Description = "Woolly Mammoth", City = "Frankfurt" },
              new Review { ReviewId = 2, Rating = 4, Description = "Dinosaur", City = "Portland" },
              new Review { ReviewId = 3, Rating = 3, Description = "Dinosaur", City = "Seattle" },
              new Review { ReviewId = 4, Rating = 4, Description = "Woolly Mammoth", City = "Cairo"},
              new Review { ReviewId = 5, Rating = 5, Description = "Dinosaur", City = "Austin" }
             );
        }
    }
}