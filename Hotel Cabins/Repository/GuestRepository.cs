using Hotel_Cabins.Data;
using Hotel_Cabins.Models;
using Hotel_Cabins.Repository.IRepository;

namespace Hotel_Cabins.Repository
{
    public class GuestRepository : Repository<Guest>,IGuestRepository
    {
        public GuestRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
