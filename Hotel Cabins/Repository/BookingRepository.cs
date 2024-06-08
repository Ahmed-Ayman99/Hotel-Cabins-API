using Hotel_Cabins.Data;
using Hotel_Cabins.Models;
using Hotel_Cabins.Repository.IRepository;

namespace Hotel_Cabins.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
