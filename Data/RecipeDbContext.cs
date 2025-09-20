using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace PF_LAB4_31A3_OBIS_FRANCINE.Data
{
    public class RecipeDBContext : DbContext
    {
        public RecipeDBContext(DbContextOptions<RecipeDBContext> options)
            : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //        public class Recipe
            //{   [Key]
            //    public int id { get; set; }

            //    [Required]
            //    [StringLength(100)]
            //    public string Name { get; set; }

            //    [StringLength(500)]
            //    public string Ingredients { get; set; }
            //}

            modelBuilder.Entity<Recipe>().HasKey(x => x.Id);
            modelBuilder.Entity<Recipe>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Recipe>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Recipe>().HasData(new[]
            {
                new Recipe
                {
                    Id = 1,
                    Name = "Binangkal",
                    Ingredients =  ""
                },
                new Recipe
                {
                    Id = 2,
                    Name = "Adobo",
                    Ingredients =  ""
                },
                new Recipe
                {
                    Id = 3,
                    Name = "",
                    Ingredients =  ""
                }
            });
        }
    }
}