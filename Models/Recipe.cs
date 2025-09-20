using System.ComponentModel.DataAnnotations;

namespace PF_LAB4_31A3_OBIS_FRANCINE.Data
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Ingredients { get; set; }
    }
};