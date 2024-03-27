using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.ViewModels;

namespace hotel_app.Services
{
    public static class RoomModelViewMapper
    {
        

        public static RoomViewModel? MapToRoomViewModel(Room room)
        {
            if (room == null)
                return null;

            return new RoomViewModel
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                HotelName = room.Hotel.Name,
                CategoryName = room.RoomCategory.Name,
                Category = room.Category,
                CreatedDate = room.CreatedDate,
                NumOfBeds = room.NumOfBeds,
                NumOfRooms = room.NumOfRooms,
                PricePerNight = room.PricePerNight, 
                IsDeleted = room.isDeleted,
            };
        }
  
        public static Room MapToRoom(RoomViewModel room, IWebHostEnvironment _hostEnvironment)
        {
            if (room == null)
                return null;

            // Mapping for Image
            string fileName = "";
            string uploadFile = Path.Combine(_hostEnvironment.WebRootPath, "img");
            fileName = Guid.NewGuid().ToString() + "_" + room.Image.FileName;
            string filePath = Path.Combine(uploadFile, fileName);
            room.Image.CopyTo(new FileStream(filePath, FileMode.Create));


            return new Room
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                HotelId = room.HotelId,
                Category = room.Category,     
                CreatedDate = room.CreatedDate,
                NumOfBeds = room.NumOfBeds,
                NumOfRooms = room.NumOfRooms,
                PricePerNight = room.PricePerNight,
                Image = fileName, 
                isDeleted = room.IsDeleted,
                
            };
        }

        public static Room MapToRoom(RoomViewModel room)
        {
            if (room == null)
                return null;

            return new Room
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                HotelId = room.HotelId,
                Category = room.Category,
                CreatedDate = room.CreatedDate,
                NumOfBeds = room.NumOfBeds,
                NumOfRooms = room.NumOfRooms,
                PricePerNight = room.PricePerNight,
                isDeleted = room.IsDeleted,
                Image = room.Image.ToString()

            };
        }


    }
}
