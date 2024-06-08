using Microsoft.AspNetCore.Identity;

namespace Hotel_Cabins.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }

    }
}
