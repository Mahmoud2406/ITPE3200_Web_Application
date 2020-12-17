using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace mappe3.DAL
{
    public class Ratings
    {
        [Key]
        public int Id { get; set; } 
        public int Rating { get; set; }
    }
    
    public class Questions
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Category { get; set; }
        virtual public Ratings Rating { get; set; }

    }

 


    public class FaqContext : DbContext
    {
            public FaqContext(DbContextOptions<FaqContext> options)
                    : base(options)
            {
                Database.EnsureCreated();
        }

        public DbSet<Questions> Questions { get; set; }
        public DbSet<Ratings> Ratings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
