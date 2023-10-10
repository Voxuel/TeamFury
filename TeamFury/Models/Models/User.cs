using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class User : IdentityUser
	{
		public ICollection<LeaveDays> LeaveDays { get; set; }

	}
}
