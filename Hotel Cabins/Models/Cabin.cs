using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Cabins.Models
{
    public class Cabin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A cabin must have a name")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name length must be between 5 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A cabin must have a max capacity")]
        [Range(1, int.MaxValue, ErrorMessage = "Max capacity must be at least 1")]
        public int MaxCapacity { get; set; }

        [Required(ErrorMessage = "A cabin must have a price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }

        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        public double? Discount { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description length cannot exceed 500 characters")]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
    }
}
