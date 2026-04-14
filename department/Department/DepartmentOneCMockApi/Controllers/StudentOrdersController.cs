using DepartmentOneCMockApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentOneCMockApi.Controllers
{
    [ApiController]
    [Route("student-orders")]
    public class StudentOrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudentOrders()
        {
            return Ok(OneCTestData.StudentOrders);
        }
    }
}