using Hotel_Cabins.DTOs.CabinsDTOs;
using Hotel_Cabins.DTOs.GuestDOTs;
using Hotel_Cabins.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Cabins.DTOs.BookingDTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int NumNights { get; set; }

        public int NumGuests { get; set; }

        public string Status { get; set; }

        public bool HasBreakfast { get; set; }
        public bool IsPaid { get; set; } = false;

        public string Observations { get; set; }
        public decimal CabinPrice { get; set; }

        public decimal ExtraPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CabinDTO Cabin { get; set; }
        public GuestDTO Guest { get; set; }
    }
}
