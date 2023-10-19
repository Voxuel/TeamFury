using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class Request
	{
        [Key]
        public int RequestID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestSent { get; set; } = DateTime.Now;
        public string? MessageForDecline { get; set; }
        public RequestType RequestType { get; set; }
        public StatusRequest StatusRequest { get; set; } = 0;
        public string? AdminName { get; set; }
    }
    public enum StatusRequest
    {
        Pending, Accepted, Declined
    }
}
