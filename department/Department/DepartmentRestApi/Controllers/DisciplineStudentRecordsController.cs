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
    public class DisciplineStudentRecordsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDisciplineStudentRecordLogic _disciplineStudentRecord;

        public DisciplineStudentRecordsController(ILogger<DisciplineStudentRecordsController> logger, IDisciplineStudentRecordLogic disciplineStudentRecord)
        {
            _logger = logger;
            _disciplineStudentRecord = disciplineStudentRecord;
        }

        [HttpGet]
        public IActionResult GetDisciplineStudentRecordList()
        {
            try
            {
                var list = _disciplineStudentRecord.ReadList(null);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading list of disciplineStudentRecords");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetDisciplineStudentRecord([FromQuery] DisciplineStudentRecordSearchModel model)
        {
            try
            {
                var element = _disciplineStudentRecord.ReadElement(model);
                return element == null ? NotFound() : Ok(element);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during reading disciplineStudentRecord element");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineStudentRecordCreate([FromBody] DisciplineStudentRecordBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _disciplineStudentRecord.Create(model);
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
                _logger.LogError(ex, "Error during disciplineStudentRecord creation");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineStudentRecordUpdate([FromBody] DisciplineStudentRecordBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _disciplineStudentRecord.Update(model);
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
                _logger.LogError(ex, "Error during disciplineStudentRecord update");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineStudentRecordDelete([FromBody] DisciplineStudentRecordBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (model.Id <= 0)
                    return BadRequest("Invalid disciplineStudentRecord ID");

                var result = _disciplineStudentRecord.Delete(model);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during disciplineStudentRecord deletion");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}
