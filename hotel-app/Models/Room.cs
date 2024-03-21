using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_app.Models
{
	public class Room
	{
		[Key]
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }

		[ForeignKey("Hotel")]
		public int HotelId { get; set; }

		[ForeignKey("RoomCategory")]
		public int Category { get; set;} //standard , VIP , LUXURY .. 
		public int NumOfBeds { get; set; }
		public decimal PricePerNight { get; set; }
		public int NumOfRooms { get; set; }
		public DateTime? CreatedDate { get; set; }
		public bool isDeleted { get; set; }
		public RoomCategory RoomCategory {  get; set; } 
		public Hotel Hotel { get; set; }
		public List<GuestRoom> GuestRooms { get; set; }

	}
}
