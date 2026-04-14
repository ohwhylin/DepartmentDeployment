using DepartmentOneCMockApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentOneCMockApi.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(OneCTestData.Students);
        }

        [HttpGet("groups")]
        public IActionResult GetStudentGroups()
        {
            return Ok(OneCTestData.StudentGroups);
        }

        [HttpGet("marks")]
        public IActionResult GetStudentMarks()
        {
            return Ok(OneCTestData.DisciplineStudentRecords);
        }
    }
}