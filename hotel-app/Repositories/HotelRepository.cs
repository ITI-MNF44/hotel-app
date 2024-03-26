using HelperClass;
using hotel_app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using hotel_app.ViewModels;
using NuGet.Protocol.Core.Types;


namespace hotel_app.Repositories
{
    public class HotelRepository : GeneralRepository<Hotel>, IHotelRepository
    {
        HotelDbContext DbContext;
        IRoomRepository roomRepository;
        UserManager<ApplicationUser> usermanager;
        IWebHostEnvironment myEnvironment;
        IGeneralRepository<Hotel> repository;
        public HotelRepository(HotelDbContext _DbContext, IRoomRepository _roomRepository,
            UserManager<ApplicationUser> _usermanager, IWebHostEnvironment _myEnvironment, IGeneralRepository<Hotel> _repository) : base(_DbContext)
        {   
            DbContext = _DbContext;
            roomRepository = _roomRepository;
            usermanager = _usermanager;
            myEnvironment = _myEnvironment;
            repository = _repository;
        }
        //Hotel register
       

        Task<Hotel> IHotelRepository.GetHotelByUserId(string userId)
        {
            return DbContext.Hotels.FirstOrDefaultAsync(h => h.UserId == userId);
        }

        public List<Room> getReservationsDetails(int hotelId)
        {
            // get hotel id == hotelId
            // load rooms navigator property of hotel,
            // load  GuestRooms navigator property of rooms navigator property 
            // load  RoomCategory navigator property of rooms navigator property 

            var hotel = DbContext.Hotels
                .Where(x => x.Id == hotelId)
                .Include(x => x.Rooms!)
                    .ThenInclude(g => g!.GuestRooms)
                .Include(x => x.Rooms!)
                    .ThenInclude(c => c.RoomCategory)
                .FirstOrDefault();

            //get hotel rooms that has a reservation and ensure that is a current reservation
            var rooms = hotel.Rooms
            .Where(room => DbContext.GuestsRooms.Any(gr => gr.RoomId == room.Id && gr.EndDate >= TimeHelperClass.getCurrTime()));
            return rooms.ToList();
        }

        public List<GuestRoom> getRoomReservationsDetails(int id)
        {
            // get room with id = id
            var room = roomRepository.GetById(id);

            // for retrieved room
            // load navigation property GuestRooms
            // load navigation properties (Guest, Room, Food) of navigation property GuestRooms
            // load navigation property Category of Food
            var Reservations = DbContext.Entry(room).Collection(b => b.GuestRooms)
                .Query()
               .Include(gr => gr.Guest)
               .Include(gr => gr.Room)
               .Include(gr => gr.Food)
               .ThenInclude(x => x.Category)
                .Where(d => d.EndDate > TimeHelperClass.getCurrTime()).ToList();

            return Reservations;
        }

        public async Task RegisterInsert(RegisterUserViewModel hotelvm)
        {
            //first add to user table
            ApplicationUser user = new ApplicationUser()
            {
                UserName = hotelvm.UserName,
                Email = hotelvm.Email
            };
            IdentityResult userCreationResult = await usermanager.CreateAsync(user, hotelvm.Password); 
            if(userCreationResult.Succeeded)
            {

            }
            string userId = user.Id;
            string filename = string.Empty;
            if(hotelvm.Image != null)
            {
                string Uploader = Path.Combine(myEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + hotelvm.Image.FileName;
                string filepath = Path.Combine(Uploader, filename);
                // Copy image file
                hotelvm.Image.CopyTo(new FileStream(filepath, FileMode.Create));

            }
            //save the rest of other data 
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
            //save
            // Add the hotel entity to the context
            repository.Insert(hotel);
            repository.Save();
        }
    }
}
