using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class RequestLog
	{
        public int RequestLogID { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
