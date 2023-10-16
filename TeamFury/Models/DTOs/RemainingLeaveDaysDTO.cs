using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RemainingLeaveDaysDTO
    {
        public int? DaysLeft { get; set; }
        public string LeaveType { get; set;}
    }
}
