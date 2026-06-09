using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentContracts.ViewModels.StudentGroups;
using DepartmentDataModels.Enums;
using DepartmentUserApp.ViewModels.StudentGroups;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DepartmentUserApp.Controllers
{
    public class StudentGroupsController : Controller
    {
        [HttpGet]
        public IActionResult List(
    string? groupSearch,
    string? studentSearch,
    AcademicCourse? course,
    bool onlyGroupsWithDebts = false,
    bool onlyStudentsWithDebts = false,
    bool onlyHighRisk = false,
    int page = 1,
    int pageSize = 10)
        {
            try
            {
                if (page < 1)
                {
                    page = 1;
                }

                if (pageSize <= 0)
                {
                    pageSize = 10;
                }

                var groups = APIClient.GetRequest<List<StudentGroupViewModel>>(
                    "api/core/StudentGroups/GetStudentGroupList")
                    ?? new List<StudentGroupViewModel>();

                var students = APIClient.GetRequest<List<StudentViewModel>>(
                    "api/core/Students/GetStudentList")
                    ?? new List<StudentViewModel>();

                var disciplineStudentRecords = APIClient.GetRequest<List<DisciplineStudentRecordViewModel>>(
                    "api/core/DisciplineStudentRecords/GetDisciplineStudentRecordList")
                    ?? new List<DisciplineStudentRecordViewModel>();

                var normalizedGroupSearch = (groupSearch ?? string.Empty).Trim();
                var normalizedStudentSearch = (studentSearch ?? string.Empty).Trim();

                var items = new List<StudentGroupListItemViewModel>();

                foreach (var group in groups
                             .OrderBy(x => x.GroupName))
                {
                    if (course.HasValue && group.Course != course.Value)
                    {
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(normalizedGroupSearch) &&
                        !(group.GroupName ?? string.Empty).Contains(normalizedGroupSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var fullGroupStudents = students
                        .Where(x => x.StudentGroupId == group.Id)
                        .OrderBy(x => x.LastName)
                        .ThenBy(x => x.FirstName)
                        .ThenBy(x => x.Patronymic)
                        .ToList();

                    var groupStudentIds = fullGroupStudents
                        .Select(x => x.Id)
                        .ToHashSet();

                    var groupDebtRecords = disciplineStudentRecords
                        .Where(x =>
                            groupStudentIds.Contains(x.StudentId) &&
                            x.MarkType == MarkType.Неудовлетворительно)
                        .OrderBy(x => x.Semester)
                        .ToList();

                    var fullStudentItems = fullGroupStudents
                        .Select(student =>
                        {
                            var studentDebts = groupDebtRecords
                                .Where(x => x.StudentId == student.Id)
                                .OrderBy(x => x.Semester)
                                .ToList();

                            return new StudentGroupStudentListItemViewModel
                            {
                                Student = student,
                                Debts = studentDebts,
                                HasHighRiskDebt = studentDebts.Any(x => IsHighRiskDebt(group.Course, x.Semester))
                            };
                        })
                        .ToList();

                    var studentsWithDebtsCount = fullStudentItems.Count(x => x.Debts.Any());
                    var highRiskStudentsCount = fullStudentItems.Count(x => x.HasHighRiskDebt);

                    if (onlyGroupsWithDebts && studentsWithDebtsCount == 0)
                    {
                        continue;
                    }

                    if (onlyHighRisk && highRiskStudentsCount == 0)
                    {
                        continue;
                    }

                    var visibleStudents = fullStudentItems;

                    if (onlyStudentsWithDebts)
                    {
                        visibleStudents = visibleStudents
                            .Where(x => x.Debts.Any())
                            .ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(normalizedStudentSearch))
                    {
                        visibleStudents = visibleStudents
                            .Where(x => BuildStudentSearchValue(x.Student)
                                .Contains(normalizedStudentSearch, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }

                    if ((onlyStudentsWithDebts || !string.IsNullOrWhiteSpace(normalizedStudentSearch)) &&
                        visibleStudents.Count == 0)
                    {
                        continue;
                    }

                    items.Add(new StudentGroupListItemViewModel
                    {
                        Group = group,
                        StudentCount = fullGroupStudents.Count,
                        StudentsWithDebtsCount = studentsWithDebtsCount,
                        HighRiskStudentsCount = highRiskStudentsCount,
                        Students = visibleStudents
                    });
                }

                var model = new StudentGroupListPageViewModel
                {
                    GroupSearch = normalizedGroupSearch,
                    StudentSearch = normalizedStudentSearch,
                    Course = course,
                    OnlyGroupsWithDebts = onlyGroupsWithDebts,
                    OnlyStudentsWithDebts = onlyStudentsWithDebts,
                    OnlyHighRisk = onlyHighRisk,
                    Result = PagedResult<StudentGroupListItemViewModel>.Create(items, page, pageSize)
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return View(new StudentGroupListPageViewModel
                {
                    GroupSearch = groupSearch?.Trim() ?? string.Empty,
                    StudentSearch = studentSearch?.Trim() ?? string.Empty,
                    Course = course,
                    OnlyGroupsWithDebts = onlyGroupsWithDebts,
                    OnlyStudentsWithDebts = onlyStudentsWithDebts,
                    OnlyHighRisk = onlyHighRisk,
                    Result = new PagedResult<StudentGroupListItemViewModel>
                    {
                        Page = 1,
                        PageSize = pageSize <= 0 ? 10 : pageSize,
                        TotalCount = 0
                    }
                });
            }
        }

        private static string BuildStudentSearchValue(StudentViewModel student)
        {
            return string.Join(
                " ",
                new[]
                {
            student.LastName ?? string.Empty,
            student.FirstName ?? string.Empty,
            student.Patronymic ?? string.Empty
                }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        private static int GetMaxSemesterForCourse(AcademicCourse course)
        {
            return course switch
            {
                AcademicCourse.Course_1 => 2,
                AcademicCourse.Course_2 => 4,
                AcademicCourse.Course_3 => 6,
                AcademicCourse.Course_4 => 8,
                _ => 0
            };
        }

        private static int GetDebtAgeInSemesters(AcademicCourse course, Semesters debtSemester)
        {
            var currentMaxSemester = GetMaxSemesterForCourse(course);
            var age = currentMaxSemester - (int)debtSemester;
            return age < 0 ? 0 : age;
        }

        private static bool IsHighRiskDebt(AcademicCourse course, Semesters debtSemester)
        {
            return GetDebtAgeInSemesters(course, debtSemester) > 2;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Некорректный идентификатор";
                    return RedirectToAction("List");
                }

                var item = APIClient.GetRequest<StudentGroupViewModel>($"api/core/StudentGroups/GetStudentGroup?id={id}");
                if (item == null)
                {
                    TempData["Error"] = "Запись не найдена";
                    return RedirectToAction("List");
                }

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("List");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                ViewBag.LecturersList = APIClient.GetRequest<List<LecturerViewModel>>("api/core/Lecturers/GetLecturerList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.EducationDirectionsList = new List<EducationDirectionViewModel>();
                ViewBag.LecturersList = new List<LecturerViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(StudentGroupBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                    ViewBag.LecturersList = APIClient.GetRequest<List<LecturerViewModel>>("api/core/Lecturers/GetLecturerList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/StudentGroups/StudentGroupCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                ViewBag.LecturersList = APIClient.GetRequest<List<LecturerViewModel>>("api/core/Lecturers/GetLecturerList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                ViewBag.LecturersList = APIClient.GetRequest<List<LecturerViewModel>>("api/core/Lecturers/GetLecturerList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentGroupsList = new List<StudentGroupViewModel>();
                ViewBag.EducationDirectionsList = new List<EducationDirectionViewModel>();
                ViewBag.LecturersList = new List<LecturerViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(StudentGroupBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                    ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                    ViewBag.LecturersList = APIClient.GetRequest<List<LecturerViewModel>>("api/core/Lecturers/GetLecturerList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/StudentGroups/StudentGroupUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                ViewBag.LecturersList = APIClient.GetRequest<List<LecturerViewModel>>("api/core/Lecturers/GetLecturerList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            try
            {
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentGroupsList = new List<StudentGroupViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Некорректный идентификатор";
                    return RedirectToAction("Delete");
                }

                APIClient.PostRequest("api/core/StudentGroups/StudentGroupDelete", new StudentGroupBindingModel
                {
                    Id = id
                });

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Delete");
            }
        }

        [HttpPost]
        public IActionResult Sync()
        {
            try
            {
                APIClient.PostRequest("api/core/Sync/student-groups");
                APIClient.PostRequest("api/core/Sync/students");
                TempData["Success"] = "Синхронизация групп и студентов выполнена успешно.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("List");
        }
    }
}
