using Microsoft.AspNetCore.Identity;

namespace Models.Models
{
	public class User : IdentityUser
	{
		public ICollection<LeaveDays> LeaveDays { get; set; }

	}

}
