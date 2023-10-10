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
        [Required]
        public int EmplyeeID { get; set; }
        [Required]
        public int RequestTypeID { get; set; }
        public int Days { get; set; }
        public RequestType RequestType { get; set; }
    }
}
