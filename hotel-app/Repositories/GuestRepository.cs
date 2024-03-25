using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class GuestRepository : GeneralRepository<Guest>, IGuestRepository
    {
        public GuestRepository(HotelDbContext DbContext) : base(DbContext)
        {
        }
        public void empty_function()
        {
            throw new NotImplementedException();
        }
    }
}
