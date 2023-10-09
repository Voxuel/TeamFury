using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class Admin
	{
        [Key]
        public int AdminID { get; set; }
        public string UserName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
