using hotel_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using hotel_app.Repositories;
using hotel_app.ViewModels;
using System;

namespace hotel_app.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        HttpContext httpContext;
        IHotelRepository hotelRepository;
        UserManager<ApplicationUser> usermanager;
        IWebHostEnvironment myEnvironment;
        public HotelService(IHttpContextAccessor _httpContextAccessor, IHotelRepository _hotelRepository, UserManager<ApplicationUser> _usermanager,
            IWebHostEnvironment _myEnvironment)
        {
            httpContextAccessor = _httpContextAccessor;
            httpContext = httpContextAccessor.HttpContext;
            hotelRepository = _hotelRepository;
            usermanager = _usermanager;
            myEnvironment = _myEnvironment;
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
        //Register Hotel
        public async Task RegisterInsert(Hotel hotel)
        {
            await hotelRepository.RegisterInsert(hotel);
        }

        //user mapping
        public ApplicationUser MapHotelUserVmToAppUser(RegisterUserViewModel hotelvm)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = hotelvm.UserName,
                Email = hotelvm.Email,
                PasswordHash = hotelvm.Password
            };
            return user;

        }

        public async Task<Hotel> MapHotelVmToHotel(RegisterUserViewModel hotelvm,string userId)
        {
                string filename = string.Empty;
                if (hotelvm.Image != null)
                {
                    string Uploader = Path.Combine(myEnvironment.WebRootPath, "images");
                    filename = Guid.NewGuid().ToString() + "_" + hotelvm.Image.FileName;
                    string filepath = Path.Combine(Uploader, filename);
                    // Copy image file
                    hotelvm.Image.CopyTo(new FileStream(filepath, FileMode.Create));
                }
                Hotel hotel = new Hotel()
                {
                    Name = hotelvm.Name,
                    Description = hotelvm.Description,
                    Country = hotelvm.Country,
                    City = hotelvm.City,
                    Address = hotelvm.Address,
                    StarRating = hotelvm.StarRating,
                    Category = hotelvm.Category,
                    CreatedDate = DateTime.Now,
                    UserId = userId,
                    Image = filename
                };
                return hotel;
        }

        public HotelWithRoomsViewModel GetHotelWithRooms(int hotelId)
        {
            (Hotel hotel, HotelCategory hotelCategory, List<(Room room, RoomCategory roomCategory)> roomsWithCategories) = hotelRepository.GetHotelWithRooms(hotelId);
            return MAPHotelRoomsVM(hotel, roomsWithCategories.Select(x => x.room).ToList(), hotelCategory, roomsWithCategories.Select(x => x.roomCategory).FirstOrDefault());
        }
        //
        public HotelWithRoomsViewModel MAPHotelRoomsVM(Hotel hotel, List<Room> rooms, HotelCategory hotelCategory, RoomCategory roomCategory)
        {
            var model = new HotelWithRoomsViewModel
            {
                Hotel = hotel,
                HotelCategory = hotelCategory,
                Rooms = rooms,
                RoomCategory = roomCategory
            };

            return model;
        }
    }
}
