namespace hotel_app.Models
{
	public class Food
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string FoodType { get; set;}
		public decimal PricePerPerson { get; set; }
		public int CategoryId { get; set; }



	}
}
