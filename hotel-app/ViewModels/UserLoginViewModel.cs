using System.ComponentModel.DataAnnotations;

namespace hotel_app.ViewModels
{
	public class UserLoginVIewModel
	{
		public string Username { get; set; }

		[DataType(DataType.Password)]
		public string Passwrod { get; set; }
		[Display(Name ="Remember Me")]
		public bool RememberMe { get; set; }
	}
}
