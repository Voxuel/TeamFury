using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class RequestType
	{
        [Key]
        public int RequestTypeID { get; set; }
        public string Name { get; set; }
        public int? MaxDays { get; set; }
    }
}
