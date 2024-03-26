using hotel_app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


using hotel_app.Models;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class HotelRepository : GeneralRepository<Hotel>, IHotelRepository
    {
        HotelDbContext DbContext;
        public HotelRepository(HotelDbContext _DbContext) : base(_DbContext)
        {
            DbContext = _DbContext;
        }

        public void test_func()
        {
            throw new NotImplementedException();
        }

        Task<Hotel> IHotelRepository.GetHotelByUserId(string userId)
        {
            return DbContext.Hotels.FirstOrDefaultAsync(h => h.UserId == userId);
        }

        public List<Hotel> AllHotels()
        {
            return DbContext.Hotels
                .Include(hotel => hotel.HotelCategory).ToList();
        }


    }
}
