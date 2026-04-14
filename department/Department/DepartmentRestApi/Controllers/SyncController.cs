using DepartmentBusinessLogic.BusinessLogics.Sync;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly IAcademicPlanSyncLogic _academicPlanSyncLogic;
        private readonly IStudentGroupSyncLogic _studentGroupSyncLogic;
        private readonly IStudentSyncLogic _studentSyncLogic;
        private readonly IDisciplineStudentRecordSyncLogic _disciplineStudentRecordSyncLogic;
        private readonly IStudentOrderSyncLogic _studentOrderSyncLogic;

        public SyncController(
            IAcademicPlanSyncLogic academicPlanSyncLogic, 
            IStudentGroupSyncLogic studentGroupSyncLogic, 
            IStudentSyncLogic studentSyncLogic, 
            IDisciplineStudentRecordSyncLogic disciplineStudentRecordSyncLogic, 
            IStudentOrderSyncLogic studentOrderSyncLogic)
        {
            _academicPlanSyncLogic = academicPlanSyncLogic;
            _studentGroupSyncLogic = studentGroupSyncLogic;
            _studentSyncLogic = studentSyncLogic;
            _disciplineStudentRecordSyncLogic = disciplineStudentRecordSyncLogic;
            _studentOrderSyncLogic = studentOrderSyncLogic;
        }

        [HttpPost("academic-plans")]
        public async Task<IActionResult> SyncAcademicPlans()
        {
            try
            {
                await _academicPlanSyncLogic.SyncAcademicPlansAsync();
                return Ok("Academic plans synchronization completed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    details = ex.Message
                });
            }
        }

        [HttpPost("student-groups")]
        public async Task<IActionResult> SyncStudentGroups()
        {
            try
            {
                await _studentGroupSyncLogic.SyncStudentGroupsAsync();
                return Ok("Student groups synchronized successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    details = ex.Message
                });
            }
        }

        [HttpPost("students")]
        public async Task<IActionResult> SyncStudents()
        {
            try
            {
                await _studentSyncLogic.SyncStudentsAsync();
                return Ok("Students synchronized successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    details = ex.Message
                });
            }
        }

        [HttpPost("discipline-student-records")]
        public async Task<IActionResult> SyncDisciplineStudentRecords()
        {
            try
            {
                await _disciplineStudentRecordSyncLogic.SyncDisciplineStudentRecordsAsync();
                return Ok("Discipline student records synchronized successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    details = ex.Message
                });
            }
        }

        [HttpPost("student-orders")]
        public async Task<IActionResult> SyncStudentOrders()
        {
            try
            {
                await _studentOrderSyncLogic.SyncStudentOrdersAsync();
                return Ok("Student orders synchronized successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    details = ex.Message
                });
            }
        }
    }
}