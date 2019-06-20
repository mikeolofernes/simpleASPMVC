using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Time_Tracking.Models
{
    public class Employee
    {

        public Int32 UserID { get; set; }
        public String EmployeeName { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public Boolean Active { get; set; }

    }
}
