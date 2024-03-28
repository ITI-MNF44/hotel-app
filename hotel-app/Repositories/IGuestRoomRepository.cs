using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IGuestRoomRepository
    {
        public int GetBookedRoomsCount(int roomId,DateTime startDate , DateTime endDate);
    }
}
