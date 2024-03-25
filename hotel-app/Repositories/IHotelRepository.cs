using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IHotelRepository: IGeneralRepository<Hotel>
    {
        public List<Room> getReservationDetails(int id);

        public List<GuestRoom> getRoomReservationDetails(int id);
    }
}
