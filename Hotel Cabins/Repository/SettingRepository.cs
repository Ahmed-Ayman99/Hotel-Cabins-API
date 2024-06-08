using Hotel_Cabins.Data;
using Hotel_Cabins.Models;
using Hotel_Cabins.Repository.IRepository;

namespace Hotel_Cabins.Repository
{
    public class SettingRepository : Repository<Setting>, ISettingsRepository
    {
        private readonly AppDbContext _context;

        public SettingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
