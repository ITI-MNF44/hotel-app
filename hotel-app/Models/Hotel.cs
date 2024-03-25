using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Xml.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.Models
{
	public class Hotel
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Country { get; set; } //from combobox in UI
		public string City {  get; set; } //from combobox in UI based on country
		public string? Address {  get; set; } //street,area...
		//Image									 
        public string Image { get; set; }
        public int StarRating {  get; set; }
		
		[ForeignKey("HotelCategory")]
		public int Category { get; set; } //luxery , resort , standard.. 
		public DateTime? CreatedDate { get; set; }
		public bool? IsAccessible { get; set; }

		public bool? IsWorking { get; set; }
		public double? Latitude { get; set; } //for map
		public double? Longitude { get; set; } // for map
		public bool isDeleted { get; set; }

		public HotelCategory? HotelCategory { get; set; }
		public List<Room>? Rooms { get; set; }
		public List<Food>? Foods { get; set; }


		public string UserId { get; set; }
		public ApplicationUser? User { get; set; }



	}
}
