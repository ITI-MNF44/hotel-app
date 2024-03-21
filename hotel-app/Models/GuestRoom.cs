using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_app.Models
{
	public class GuestRoom
	{
		[Key]
		public int Id  { get; set; } 

		[ForeignKey("Guest")]
		public int GuestId { get; set; }
		[ForeignKey("Room")]

		public int RoomId { get; set; }
		[ForeignKey("Food")]

		public int? FoodId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime BookingDate { get; set; }
		public int RoomsAmount { get; set; }
		public int? FoodAmount { get; set; }
		public Guest Guest { get; set; }
		public Room Room { get; set; }
		public Food Food { get; set; }
	}
}
