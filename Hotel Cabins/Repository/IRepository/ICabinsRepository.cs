using Hotel_Cabins.Models;

namespace Hotel_Cabins.Repository.IRepository
{
    public interface ICabinsRepository : IRepository<Cabin>
    {
        public Task<object> GetCabinsStats();

    }
}
