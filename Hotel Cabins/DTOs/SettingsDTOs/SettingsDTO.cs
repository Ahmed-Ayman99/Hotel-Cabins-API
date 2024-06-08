using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Cabins.DTOs.SettingsDTOs
{
    public class SettingsDTO
    {
        public int Id { get; set; }
        public int MinBookingLength { get; set; }
        public int MaxBookingLength { get; set; }
        public int maxGuestsPerBooking { get; set; }
        public decimal breakFastPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
