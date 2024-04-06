using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Services
{
    public class RoomService : IRoomService
    {
        readonly IRoomRepository _roomRepository;
        readonly IRoomCategoryRepository _roomCategoryRepository;
        readonly IGuestRoomRepository _guestRoomRepository;
        private readonly IFoodService _foodService;

        public RoomService(IRoomRepository roomRepository,
        IRoomCategoryRepository roomCategoryRepository, IGuestRoomRepository guestRoomRepository, IFoodService foodService )
        {
            _roomRepository = roomRepository;
            _roomCategoryRepository = roomCategoryRepository;
            _guestRoomRepository = guestRoomRepository;
            _foodService = foodService;
        }

        public Room GetById(int id)
        {
            return _roomRepository.GetById(id);
        }

        public List<Room> HotelRooms(int id)
        {
            return _roomRepository.HotelRooms(id);
        }

        public List<RoomCategory> RoomCategories()
        {
            return _roomCategoryRepository.GetAll();
        }

        public void Insert(Room room)
        {
            _roomRepository.Insert(room);
        }

        public void Save()
        {
            _roomRepository.Save();
        }

        public Room GetById(int id, params string[] include)
        {
            return _roomRepository.GetById(id, include);
        }
        public Task<Room?> GetByIdAsync(int id, params string[] include)
        {
            return _roomRepository.GetByIdAsync(id, include);
        }

        public void Update(Room room)
        {
            _roomRepository.Update(room);
        }
        public void Delete(int id)
        {
            _roomRepository.Delete(id);
        }

        public List<Room> AllAvailableRooms()
        {
            return _roomRepository.AllAvailableRooms();
        }

        public async Task<bool> isRoomAvailable(int id, int amount, DateTime startDate, DateTime endDate)
        {
            Room room = await _roomRepository.GetByIdAsync(id);
            if (room != null)
            {
                int numOfBooked = _guestRoomRepository.GetBookedRoomsCount(id, startDate, endDate);
                if (numOfBooked + amount < room.NumOfRooms)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<BookingDetailsViewModel> GetBookingRoomVM(int id)
        {
            Room room = await GetByIdAsync(id, "RoomCategory", "Hotel");
            var foods = await _foodService.GetHotelFoodsAsync(room.Hotel.Id);
            return new BookingDetailsViewModel()
            {
                RoomId = room.Id,
                RoomName = room.Name,
                Image = room.Image,
                HotelName = room.Hotel.Name,
                HotelId = room.Hotel.Id,
                Country = room.Hotel.Country,
                City = room.Hotel.City,
                Address = room.Hotel.Address,
                RoomCategory = room.RoomCategory.Name,
                BedsNum = room.NumOfBeds,
                FoodList = foods,
                RoomDescription = room.Description,
                price = room.PricePerNight,
                StartDate =DateTime.Today,
                EndDate = DateTime.Today.AddDays(3),


            };
        }

        public async Task<bool> SaveBooking(int guestId, BookingDetailsViewModel viewModel)
        {
            GuestRoom booking = new GuestRoom() { 
            GuestId = guestId,
            RoomId = viewModel.RoomId,
            FoodId = viewModel.FoodId==null?null: viewModel.FoodId,
            StartDate = viewModel.StartDate,
            EndDate = viewModel.EndDate,
            BookingDate = DateTime.Now,
            RoomsAmount = viewModel.RoomsAmount,
            FoodAmount = viewModel.BedsNum
            };
            _guestRoomRepository.Insert(booking);
            _guestRoomRepository.Save();
            return true;
        }


    }
}
