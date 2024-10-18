using System.ComponentModel.DataAnnotations;

namespace HolidayHomeProject.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email is required.")]
		[MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
							ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or min 5 characters allowed.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	}
}
