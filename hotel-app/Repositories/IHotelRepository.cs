using hotel_app.Models;
using hotel_app.ViewModels;
using System.Threading.Tasks;

namespace hotel_app.Repositories
{
    public interface IHotelRepository : IGeneralRepository<Hotel>
    {
        Task RegisterInsert(RegisterUserViewModel hotelvm);
    }
}