using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Cabins.Models
{
    public class Setting
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Must have minBookingLength")]
        public int MinBookingLength { get; set; }

        [Required(ErrorMessage = "Must have MaxBookingLength ")]
        public int MaxBookingLength { get; set; }

        [Required(ErrorMessage = "Must have maxGuestsPerBooking")]
        public int maxGuestsPerBooking { get; set; }

        [Range(1, double.MaxValue), Required(ErrorMessage = "Must have breakFastPrice")]
        public decimal breakFastPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
    }
}
