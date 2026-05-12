using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.SearchModels;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthProfileController : ControllerBase
    {
        private readonly IAuthProfileLogic _logic;
        private readonly ILogger _logger;

        public AuthProfileController(
            ILogger<AuthProfileController> logger,
            IAuthProfileLogic logic)
        {
            _logger = logger;
            _logic = logic;
        }

        [HttpGet]
        public IActionResult GetProfile([FromQuery] string login)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login))
                {
                    return BadRequest("Login is empty");
                }

                var result = _logic.ReadProfile(new AuthProfileSearchModel
                {
                    Login = login.Trim()
                });

                return result == null ? NotFound() : Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during reading auth profile");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}