using System.ComponentModel.DataAnnotations;

namespace hotel_app.Models
{
	public class FoodCategory
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		public List<Food> foods { get; set; }
	}
}
