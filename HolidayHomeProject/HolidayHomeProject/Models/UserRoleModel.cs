using Microsoft.AspNetCore.Identity;

namespace HolidayHomeProject.Models
{
	public class UserRoleModel : IdentityRole<Guid>
	{
		public string Description { get; set; }
	}
}
