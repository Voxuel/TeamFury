using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RemainingLeaveDaysDTO
    {
        public int RequestTypeId { get; set; }
        public int? MaxDays { get; set; }
        public string Name { get; set;}
    }
}
