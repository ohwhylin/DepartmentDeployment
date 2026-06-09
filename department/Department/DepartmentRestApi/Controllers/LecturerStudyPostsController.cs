using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;

namespace DepartmentRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LecturerStudyPostsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILecturerStudyPostLogic _lecturerStudyPost;

        public LecturerStudyPostsController(ILogger<LecturerStudyPostsController> logger, ILecturerStudyPostLogic lecturerStudyPost)
        {
            _logger = logger;
            _lecturerStudyPost = lecturerStudyPost;
        }

        [HttpGet]
        public IActionResult GetLecturerStudyPostList()
        {
            try
            {
                var list = _lecturerStudyPost.ReadList(null);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading list of lecturerStudyPosts");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetLecturerStudyPostPage([FromQuery] LecturerStudyPostSearchModel model)
        {
            try
            {
                model ??= new LecturerStudyPostSearchModel();

                if (model.Page < 1)
                {
                    model.Page = 1;
                }

                if (model.PageSize <= 0)
                {
                    model.PageSize = 10;
                }

                var list = _lecturerStudyPost.ReadList(model) ?? new List<LecturerStudyPostViewModel>();
                var result = PagedResult<LecturerStudyPostViewModel>.Create(list, model.Page, model.PageSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading page of lecturerStudyPosts");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetLecturerStudyPost([FromQuery] LecturerStudyPostSearchModel model)
        {
            try
            {
                var element = _lecturerStudyPost.ReadElement(model);
                return element == null ? NotFound() : Ok(element);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during reading lecturerStudyPost element");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult LecturerStudyPostCreate([FromBody] LecturerStudyPostBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _lecturerStudyPost.Create(model);
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
                _logger.LogError(ex, "Error during lecturerStudyPost creation");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult LecturerStudyPostUpdate([FromBody] LecturerStudyPostBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _lecturerStudyPost.Update(model);
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
                _logger.LogError(ex, "Error during lecturerStudyPost update");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult LecturerStudyPostDelete([FromBody] LecturerStudyPostBindingModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model is null");

                if (model.Id <= 0)
                    return BadRequest("Invalid lecturerStudyPost ID");

                var result = _lecturerStudyPost.Delete(model);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during lecturerStudyPost deletion");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}