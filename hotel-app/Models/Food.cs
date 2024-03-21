using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_app.Models
{
	public class Food
	{
		[Key]
		public int Id { get; set; }
		
		[ForeignKey("Hotel")]
		public int HotelID { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public decimal PricePerPerson { get; set; }

		[ForeignKey("Category")]

		public int CategoryId { get; set; }


		public DateTime? CreatedDate { get; set; }
		public bool isDeleted { get; set; }


		public FoodCategory Category { get; set; }
		public Hotel Hotel { get; set; }

		public List<GuestRoom> GuestRooms { get; set;}




	}
}
