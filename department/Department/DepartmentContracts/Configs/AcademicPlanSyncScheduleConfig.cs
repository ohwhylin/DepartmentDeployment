using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentContracts.Configs
{
    public class AcademicPlanSyncScheduleConfig
    {
        public bool Enabled { get; set; }
        public string DayOfWeek { get; set; } = "Friday";
        public int Hour { get; set; } = 0;
        public int Minute { get; set; } = 25;
    }
}
