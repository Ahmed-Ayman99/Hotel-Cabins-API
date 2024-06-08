using System.ComponentModel.DataAnnotations;

namespace Hotel_Cabins.DTOs.SettingsDTOs
{
    public class SettingsCreateDTO
    {
        [Required(ErrorMessage = "Must have minBookingLength")]
        public int MinBookingLength { get; set; }

        [Required(ErrorMessage = "Must have MaxBookingLength ")]
        public int MaxBookingLength { get; set; }

        [Required(ErrorMessage = "Must have maxGuestsPerBooking")]
        public int maxGuestsPerBooking { get; set; }

        [Range(1, double.MaxValue), Required(ErrorMessage = "Must have breakFastPrice")]
        public decimal breakFastPrice { get; set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; } = DateTime.UtcNow;
    }
}
