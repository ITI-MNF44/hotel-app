using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public bool isDeleted { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		

	}
}
