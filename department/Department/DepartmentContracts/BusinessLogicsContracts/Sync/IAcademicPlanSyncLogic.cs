using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentContracts.BusinessLogicsContracts.Sync
{
    public interface IAcademicPlanSyncLogic
    {
        Task SyncAcademicPlansAsync();
    }
}
