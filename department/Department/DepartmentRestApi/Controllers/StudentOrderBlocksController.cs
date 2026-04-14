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
    public class StudentOrderBlocksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IStudentOrderBlockLogic _studentOrderBlock;

        public StudentOrderBlocksController(ILogger<StudentOrderBlocksController> logger, IStudentOrderBlockLogic studentOrderBlock)
        {
            _logger = logger;
            _studentOrderBlock = studentOrderBlock;
        }

        [HttpGet]
        public IActionResult GetStudentOrderBlockList()
        {
            try
            {
                var list = _studentOrderBlock.ReadList(null);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading list of studentOrderBlocks");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetStudentOrderBlock([FromQuery] StudentOrderBlockSearchModel model)
        {
            try
            {
                var element = _studentOrderBlock.ReadElement(model);
                return element == null ? NotFound() : Ok(element);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during reading studentOrderBlock element");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult StudentOrderBlockCreate([FromBody] StudentOrderBlockBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _studentOrderBlock.Create(model);
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
                _logger.LogError(ex, "Error during studentOrderBlock creation");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult StudentOrderBlockUpdate([FromBody] StudentOrderBlockBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _studentOrderBlock.Update(model);
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
                _logger.LogError(ex, "Error during studentOrderBlock update");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult StudentOrderBlockDelete([FromBody] StudentOrderBlockBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (model.Id <= 0)
                    return BadRequest("Invalid studentOrderBlock ID");

                var result = _studentOrderBlock.Delete(model);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during studentOrderBlock deletion");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}
