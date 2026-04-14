using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentOrderBlockStudentsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IStudentOrderBlockStudentLogic _studentOrderBlockStudent;

        public StudentOrderBlockStudentsController(ILogger<StudentOrderBlockStudentsController> logger, IStudentOrderBlockStudentLogic studentOrderBlockStudent)
        {
            _logger = logger;
            _studentOrderBlockStudent = studentOrderBlockStudent;
        }

        [HttpGet]
        public IActionResult GetStudentOrderBlockStudentList()
        {
            try
            {
                var list = _studentOrderBlockStudent.ReadList(null);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading list of studentOrderBlockStudents");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetStudentOrderBlockStudent([FromQuery] StudentOrderBlockStudentSearchModel model)
        {
            try
            {
                var element = _studentOrderBlockStudent.ReadElement(model);
                return element == null ? NotFound() : Ok(element);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during reading studentOrderBlockStudent element");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult StudentOrderBlockStudentCreate([FromBody] StudentOrderBlockStudentBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _studentOrderBlockStudent.Create(model);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during studentOrderBlockStudent creation");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult StudentOrderBlockStudentUpdate([FromBody] StudentOrderBlockStudentBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _studentOrderBlockStudent.Update(model);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during studentOrderBlockStudent update");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult StudentOrderBlockStudentDelete([FromBody] StudentOrderBlockStudentBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (model.Id <= 0)
                    return BadRequest("Invalid studentOrderBlockStudent ID");

                var result = _studentOrderBlockStudent.Delete(model);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during studentOrderBlockStudent deletion");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}
