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
    public class LecturerDepartmentPostsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILecturerDepartmentPostLogic _lecturerDepartmentPost;

        public LecturerDepartmentPostsController(ILogger<LecturerDepartmentPostsController> logger, ILecturerDepartmentPostLogic lecturerDepartmentPost)
        {
            _logger = logger;
            _lecturerDepartmentPost = lecturerDepartmentPost;
        }

        [HttpGet]
        public IActionResult GetLecturerDepartmentPostList()
        {
            try
            {
                var list = _lecturerDepartmentPost.ReadList(null);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading list of lecturerDepartmentPosts");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetLecturerDepartmentPost([FromQuery] LecturerDepartmentPostSearchModel model)
        {
            try
            {
                var element = _lecturerDepartmentPost.ReadElement(model);
                return element == null ? NotFound() : Ok(element);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during reading lecturerDepartmentPost element");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult LecturerDepartmentPostCreate([FromBody] LecturerDepartmentPostBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _lecturerDepartmentPost.Create(model);
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
                _logger.LogError(ex, "Error during lecturerDepartmentPost creation");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult LecturerDepartmentPostUpdate([FromBody] LecturerDepartmentPostBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _lecturerDepartmentPost.Update(model);
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
                _logger.LogError(ex, "Error during lecturerDepartmentPost update");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult LecturerDepartmentPostDelete([FromBody] LecturerDepartmentPostBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (model.Id <= 0)
                    return BadRequest("Invalid lecturerDepartmentPost ID");

                var result = _lecturerDepartmentPost.Delete(model);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during lecturerDepartmentPost deletion");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}
