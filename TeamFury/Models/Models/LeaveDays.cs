using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class LeaveDays
    {
        [Key]
        public int ID { get; set; }
        
        public int Days { get; set; }
        public Request Request { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
