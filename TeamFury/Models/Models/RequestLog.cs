using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class RequestLog
	{
		[Key]
        public int RequestLogID { get; set; }
        public Request Request { get; set; }
    }
}
