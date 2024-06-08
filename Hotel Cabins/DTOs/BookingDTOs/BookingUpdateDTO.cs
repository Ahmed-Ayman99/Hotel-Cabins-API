using System.ComponentModel.DataAnnotations;

namespace Hotel_Cabins.DTOs.BookingDTOs
{
    public class BookingUpdateDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 

        [Range(1, int.MaxValue), Required(ErrorMessage = "A Booking must have a NumNights")]
        public int NumNights { get; set; }

        [Range(1, int.MaxValue), Required(ErrorMessage = "A Booking must have a NumGuests")]
        public int NumGuests { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Observations must between 5 - 50 charachter"), Required(ErrorMessage = "A Booking must have a Status")]
        public string Status { get; set; }

        public bool HasBreakfast { get; set; }
        public bool IsPaid { get; set; } = false;

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Observations must between 5 - 50 charachter"), Required(ErrorMessage = "A Booking must have a Observations")]
        public string Observations { get; set; }
        [Range(1, double.MaxValue), Required(ErrorMessage = "A Booking must have a CabinPrice")]
        public decimal CabinPrice { get; set; }

        [Range(1, double.MaxValue), Required(ErrorMessage = "A Booking must have a ExtraPrice")]
        public decimal ExtraPrice { get; set; }
        public DateTime UpdatedAt { get;} = DateTime.UtcNow;

        public int CabinId { get; set; }
        public int GuestId { get; set; }
    }
}
