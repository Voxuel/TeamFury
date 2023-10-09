using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class EmployeeRequest
	{
        [Key]
        public int ID { get; set; }
        public int RequestID { get; set; }
        public int EmployeeID { get; set; }
    }
}
