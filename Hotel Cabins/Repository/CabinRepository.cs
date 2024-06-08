using Hotel_Cabins.Data;
using Hotel_Cabins.Models;
using Hotel_Cabins.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Cabins.Repository
{
    public class CabinRepository : Repository<Cabin>, ICabinsRepository
    {
        private readonly AppDbContext _context;

        public CabinRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<object> GetCabinsStats() {
            var stats =await _context.Cabins
                .GroupBy(cabin=>1)
                .Select(cabins=> new {
                    numCabins= cabins.Count(), 
                    AvgMaxCapacity = cabins.Average(cabin => cabin.MaxCapacity),
                    AvgPrice= cabins.Average(cabin => cabin.Price),
                    AvgDiscount= cabins.Average(cabin => cabin.Discount),
                }).ToListAsync();

            return stats;
        }
    }
}
