using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IGuestRepository: IGeneralRepository<Guest>
    {
        public void empty_function();
    }
}
