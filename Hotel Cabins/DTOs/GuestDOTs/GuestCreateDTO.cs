using System.ComponentModel.DataAnnotations;

namespace Hotel_Cabins.DTOs.GuestDOTs
{
    public class GuestCreateDTO
    {
            [Required(ErrorMessage = "A Guest must have a FullName")]
            public string FullName { get; set; }

            [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A Guest must have a nationality")]
            public string Nationality { get; set; }

            [Required(ErrorMessage = "A Guest must have a national ID")]
            public string NationalID { get; set; }

            [Required(ErrorMessage = "A Guest must have a country flag")]
            public string CountryFlag { get; set; }
    }
}
