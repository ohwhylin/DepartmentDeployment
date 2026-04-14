using DepartmentOneCMockApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentOneCMockApi.Controllers
{
    [ApiController]
    [Route("academic-plans")]
    public class AcademicPlansController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAcademicPlans()
        {
            return Ok(OneCTestData.AcademicPlans);
        }
    }
}