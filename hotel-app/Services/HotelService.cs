using hotel_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using hotel_app.Repositories;

namespace hotel_app.Services
{
    public class HotelService:IHotelService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        HttpContext httpContext;
        IHotelRepository hotelRepository;
        public HotelService(IHttpContextAccessor _httpContextAccessor,IHotelRepository _hotelRepository)
        {
            httpContextAccessor = _httpContextAccessor;
            httpContext = httpContextAccessor.HttpContext;
            hotelRepository = _hotelRepository;
        }
        public async Task<Hotel> GetCurrentHotel()
        {
            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            // Find the claim representing the user ID
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            // return hotel
            return await hotelRepository.GetHotelByUserId(userIdClaim.Value);
        }
    }
}
