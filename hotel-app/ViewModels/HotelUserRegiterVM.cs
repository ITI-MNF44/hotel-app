using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_app.ViewModels
{
	public class HotelUserRegiterVM
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Country { get; set; } //from combobox in UI
		public string City { get; set; } //from combobox in UI based on country
		public string? Address { get; set; } //street,area...
		public int StarRating { get; set; } // 4,5,.. starts

		[ForeignKey("HotelCategory")]
		public int Category { get; set; } //luxery , resort , standard.. 
		public bool? IsAccessible { get; set; }
		public string UserName { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

	}
}
