using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IGuestRepository: IGeneralRepository<Guest>
    {
        public Task<Guest> GetGuestByUserId(string userId);

    }
}
