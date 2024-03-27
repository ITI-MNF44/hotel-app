using hotel_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using hotel_app.Repositories;
using hotel_app.ViewModels;

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

        public List<Hotel> AllHotels()
        {
            return hotelRepository.AllHotels();
        }

        public async Task<Hotel> GetCurrentHotel()
        {
            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            // Find the claim representing the user ID
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            // return hotel
            return await hotelRepository.GetHotelByUserId(userIdClaim.Value);
        }

        public List<Room> ReservationsInfo(int id)
        {
            return hotelRepository.getReservationsDetails(id);
        }

        public List<RoomGuestReservationVM> RoomReservationsDetails(int id)
        {
            var RoomReservations = hotelRepository.getRoomReservationsDetails(id);
            
            return hotelRepository.getRoomReservationsDetails(id).Select(x => new RoomGuestReservationVM()
            {
                Full_name = x.Guest.FirstName + " " + x.Guest.LastName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                BookingDate = x.BookingDate,
                Rooms_amount = x.RoomsAmount,
                RoomPrice = x.Room.PricePerNight,
                FoodAmount = x.FoodAmount,
                FoodCategory = x.Food == null ? null : x.Food.Category.Name,
                FoodName = x.Food == null ? null : x.Food.Name,
                FoodPrice = x.Food == null ? null : x.Food.PricePerPerson,

            }).ToList();

        }
    }
}
