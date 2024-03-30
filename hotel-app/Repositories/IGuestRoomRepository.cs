using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IGuestRoomRepository: IGeneralRepository<GuestRoom>
    {
        public int GetBookedRoomsCount(int roomId,DateTime startDate , DateTime endDate);
        
    }
}
