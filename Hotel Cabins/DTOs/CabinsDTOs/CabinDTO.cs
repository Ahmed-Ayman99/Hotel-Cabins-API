using System.ComponentModel.DataAnnotations;

namespace Hotel_Cabins.DTOs.CabinsDTOs
{
    public class CabinDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
    }
}
