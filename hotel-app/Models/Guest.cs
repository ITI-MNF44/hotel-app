using System.ComponentModel.DataAnnotations;

namespace hotel_app.Models
{
	public class Guest
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Country { get; set; }
		public string Gender { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime? CreatedDate { get; set; }
		public List<GuestRoom> BookedRooms { get; set; }

	}
}
