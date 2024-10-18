using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HolidayHomeProject.Models
{
	public class UserModel : IdentityUser
	{
		[MaxLength(50)]
		public string? firstName { get; set; }

		[MaxLength(50)]
		public string? lastName { get; set; }

		public bool isAdmin { get; set; } = false;
	}
}
