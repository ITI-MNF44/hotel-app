using System.ComponentModel.DataAnnotations;

namespace hotel_app.Models
{
	public class HotelCategory
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Hotel> hotels { get; set; }
	}
}
