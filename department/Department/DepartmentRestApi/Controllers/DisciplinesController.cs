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
    public class DisciplinesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDisciplineLogic _discipline;

        public DisciplinesController(ILogger<DisciplinesController> logger, IDisciplineLogic discipline)
        {
            _logger = logger;
            _discipline = discipline;
        }

        [HttpGet]
        public IActionResult GetDisciplineList()
        {
            try
            {
                var list = _discipline.ReadList(null);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading list of disciplines");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetDiscipline([FromQuery] DisciplineSearchModel model)
        {
            try
            {
                var element = _discipline.ReadElement(model);
                return element == null ? NotFound() : Ok(element);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during reading discipline element");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineCreate([FromBody] DisciplineBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _discipline.Create(model);
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
                _logger.LogError(ex, "Error during discipline creation");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineUpdate([FromBody] DisciplineBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _discipline.Update(model);
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
                _logger.LogError(ex, "Error during discipline update");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineDelete([FromBody] DisciplineBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (model.Id <= 0)
                    return BadRequest("Invalid discipline ID");

                var result = _discipline.Delete(model);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during discipline deletion");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}
