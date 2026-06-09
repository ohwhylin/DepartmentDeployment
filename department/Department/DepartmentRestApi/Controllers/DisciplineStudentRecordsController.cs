using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IDisciplineLogic _discipline;
        private readonly IStudentLogic _student;
        private readonly IStudentGroupLogic _studentGroup;

        public DisciplineStudentRecordsController(
            ILogger<DisciplineStudentRecordsController> logger,
            IDisciplineStudentRecordLogic disciplineStudentRecord,
            IDisciplineLogic discipline,
            IStudentLogic student,
            IStudentGroupLogic studentGroup)
        {
            _logger = logger;
            _disciplineStudentRecord = disciplineStudentRecord;
            _discipline = discipline;
            _student = student;
            _studentGroup = studentGroup;
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
        public IActionResult GetDisciplineStudentRecordPage([FromQuery] DisciplineStudentRecordSearchModel model)
        {
            try
            {
                model ??= new DisciplineStudentRecordSearchModel();

                if (model.Page < 1)
                {
                    model.Page = 1;
                }

                if (model.PageSize <= 0)
                {
                    model.PageSize = 5;
                }

                var allGroups = _studentGroup.ReadList(null) ?? new List<StudentGroupViewModel>();
                var allStudents = _student.ReadList(null) ?? new List<StudentViewModel>();
                var allRecords = _disciplineStudentRecord.ReadList(null) ?? new List<DisciplineStudentRecordViewModel>();
                var allDisciplines = _discipline.ReadList(null) ?? new List<DisciplineViewModel>();

                IEnumerable<StudentGroupViewModel> filteredGroups = allGroups;

                if (!string.IsNullOrWhiteSpace(model.GroupSearch))
                {
                    var groupSearch = model.GroupSearch.Trim().ToLowerInvariant();

                    filteredGroups = filteredGroups.Where(group =>
                        (group.GroupName ?? string.Empty).ToLowerInvariant().Contains(groupSearch));
                }

                if (!string.IsNullOrWhiteSpace(model.StudentSearch))
                {
                    var studentSearch = model.StudentSearch.Trim().ToLowerInvariant();

                    filteredGroups = filteredGroups.Where(group =>
                        allStudents.Any(student =>
                            student.StudentGroupId == group.Id &&
                            $"{student.LastName} {student.FirstName} {student.Patronymic}"
                                .ToLowerInvariant()
                                .Contains(studentSearch)));
                }

                var pagedGroups = PagedResult<StudentGroupViewModel>.Create(
                    filteredGroups
                        .OrderBy(x => x.GroupName)
                        .ToList(),
                    model.Page,
                    model.PageSize);

                var selectedGroupIds = pagedGroups.Items
                    .Select(x => x.Id)
                    .ToHashSet();

                var pageStudentsQuery = allStudents
                    .Where(x => x.StudentGroupId.HasValue && selectedGroupIds.Contains(x.StudentGroupId.Value));

                if (!string.IsNullOrWhiteSpace(model.StudentSearch))
                {
                    var studentSearch = model.StudentSearch.Trim().ToLowerInvariant();

                    pageStudentsQuery = pageStudentsQuery.Where(student =>
                        $"{student.LastName} {student.FirstName} {student.Patronymic}"
                            .ToLowerInvariant()
                            .Contains(studentSearch));
                }

                var pageStudents = pageStudentsQuery
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .ThenBy(x => x.Patronymic)
                    .ToList();

                var pageStudentIds = pageStudents
                    .Select(x => x.Id)
                    .ToHashSet();

                var pageRecords = allRecords
                    .Where(x => pageStudentIds.Contains(x.StudentId))
                    .OrderBy(x => x.StudentId)
                    .ThenBy(x => x.DisciplineId)
                    .ThenBy(x => x.Semester)
                    .ToList();

                var pageDisciplineIds = pageRecords
                    .Select(x => x.DisciplineId)
                    .Distinct()
                    .ToHashSet();

                var pageDisciplines = allDisciplines
                    .Where(x => pageDisciplineIds.Contains(x.Id))
                    .OrderBy(x => x.DisciplineName)
                    .ToList();

                var result = new DisciplineStudentRecordGroupPageViewModel
                {
                    GroupSearch = model.GroupSearch?.Trim() ?? string.Empty,
                    StudentSearch = model.StudentSearch?.Trim() ?? string.Empty,
                    Groups = pagedGroups,
                    Students = pageStudents,
                    Disciplines = pageDisciplines,
                    Records = pageRecords
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during loading page of disciplineStudentRecords");
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