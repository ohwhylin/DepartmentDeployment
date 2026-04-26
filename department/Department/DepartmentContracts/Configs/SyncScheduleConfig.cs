using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentContracts.Configs
{
    public class SyncScheduleConfig
    {
        public bool Enabled { get; set; } = true;
        public int Hour { get; set; } = 10;
        public int Minute { get; set; } = 0;
    }
}
