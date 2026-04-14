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
    public class DisciplineBlocksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDisciplineBlockLogic _disciplineBlock;

        public DisciplineBlocksController(ILogger<DisciplineBlocksController> logger, IDisciplineBlockLogic disciplineBlock)
        {
            _logger = logger;
            _disciplineBlock = disciplineBlock;
        }

        [HttpGet]
        public IActionResult GetDisciplineBlockList()
        {
            try
            {
                var list = _disciplineBlock.ReadList(null);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading list of disciplineBlocks");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetDisciplineBlock([FromQuery] DisciplineBlockSearchModel model)
        {
            try
            {
                var element = _disciplineBlock.ReadElement(model);
                return element == null ? NotFound() : Ok(element);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during reading disciplineBlock element");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineBlockCreate([FromBody] DisciplineBlockBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _disciplineBlock.Create(model);
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
                _logger.LogError(ex, "Error during disciplineBlock creation");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineBlockUpdate([FromBody] DisciplineBlockBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _disciplineBlock.Update(model);
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
                _logger.LogError(ex, "Error during disciplineBlock update");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DisciplineBlockDelete([FromBody] DisciplineBlockBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (model.Id <= 0)
                    return BadRequest("Invalid disciplineBlock ID");

                var result = _disciplineBlock.Delete(model);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during disciplineBlock deletion");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}
