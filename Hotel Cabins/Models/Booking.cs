using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Cabins.Models
{
    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);

        [Range(1,int.MaxValue), Required(ErrorMessage = "A Booking must have a NumNights")]
        public int NumNights { get; set; }
        
        [Range(1,int.MaxValue), Required(ErrorMessage = "A Booking must have a NumGuests")]
        public int NumGuests { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Observations must between 5 - 50 charachter"), Required(ErrorMessage = "A Booking must have a Status")]
        public string Status { get; set; }

        public bool HasBreakfast { get; set; }
        public bool IsPaid { get; set; }=false;
        
        [StringLength(50, MinimumLength = 5 ,ErrorMessage = "Observations must between 5 - 50 charachter"), Required(ErrorMessage = "A Booking must have a Observations")]
        public string Observations { get; set; }
        [Range(1,double.MaxValue), Required(ErrorMessage = "A Booking must have a CabinPrice")]
        public decimal CabinPrice { get; set; }

        [Range(1,double.MaxValue), Required(ErrorMessage = "A Booking must have a ExtraPrice")]
        public decimal ExtraPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relation
        [ForeignKey("Cabin")]
        public int CabinId { get; set; }
        public Cabin Cabin { get; set; }

        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }

}
