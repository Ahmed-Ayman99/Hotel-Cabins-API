using System.ComponentModel.DataAnnotations;

namespace Hotel_Cabins.DTOs.SettingsDTOs
{
    public class SettingsUpdateDTO
    {
        public int Id  { get; set; }
        [Range(1, int.MaxValue) , Required(ErrorMessage = "Must have minBookingLength")]
        public int MinBookingLength { get; set; }

        [Range(1, int.MaxValue), Required(ErrorMessage = "Must have MaxBookingLength")]
        public int MaxBookingLength { get; set; }

        [Range(1, int.MaxValue), Required(ErrorMessage = "Must have maxGuestsPerBooking")]
        public int maxGuestsPerBooking { get; set; }

        [Range(1, double.MaxValue), Required(ErrorMessage = "Must have breakFastPrice")]
        public decimal breakFastPrice { get; set; }
        public DateTime UpdatedAt { get; } = DateTime.UtcNow;
    }
}
