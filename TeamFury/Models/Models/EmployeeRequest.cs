using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models.Models
{
    public class EmployeeRequest
    {
        [Key] public int Id { get; set; }

        public Request Request { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}