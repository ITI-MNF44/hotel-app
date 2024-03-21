using System.ComponentModel.DataAnnotations;

namespace hotel_app.Models
{
	public class RoomCategory
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		public List<Room> rooms { get; set; }
	}
}
