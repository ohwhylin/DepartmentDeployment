using Microsoft.AspNetCore.Mvc;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepartmentUserApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var educationDirections =
                    APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList")
                    ?? new List<EducationDirectionViewModel>();

                var lecturers =
                    APIClient.GetRequest<List<LecturerViewModel>>("api/core/Lecturers/GetLecturerList")
                    ?? new List<LecturerViewModel>();

                var students =
                    APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList")
                    ?? new List<StudentViewModel>();

                var studentGroups =
                    APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList")
                    ?? new List<StudentGroupViewModel>();

                var disciplines =
                    APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList")
                    ?? new List<DisciplineViewModel>();

                var academicPlans =
                    APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList")
                    ?? new List<AcademicPlanViewModel>();

                var studentOrders =
                    APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList")
                    ?? new List<StudentOrderViewModel>();

                var classrooms =
                    APIClient.GetRequest<List<ClassroomViewModel>>("api/core/Classrooms/GetClassroomList")
                    ?? new List<ClassroomViewModel>();

                var disciplineStudentRecords =
                    APIClient.GetRequest<List<DisciplineStudentRecordViewModel>>("api/core/DisciplineStudentRecords/GetDisciplineStudentRecordList")
                    ?? new List<DisciplineStudentRecordViewModel>();

                ViewBag.EducationDirectionCount = educationDirections.Count;
                ViewBag.LecturerCount = lecturers.Count;
                ViewBag.StudentCount = students.Count;
                ViewBag.StudentGroupCount = studentGroups.Count;
                ViewBag.DisciplineCount = disciplines.Count;
                ViewBag.AcademicPlanCount = academicPlans.Count;
                ViewBag.StudentOrderCount = studentOrders.Count;
                ViewBag.ClassroomCount = classrooms.Count;

                ViewBag.DisciplineStudentRecordCount = disciplineStudentRecords.Count;
                ViewBag.UnsatisfactoryCount = disciplineStudentRecords.Count(x => x.MarkType == MarkType.Неудовлетворительно);
                ViewBag.AbsentCount = disciplineStudentRecords.Count(x => x.MarkType == MarkType.Неявка);
                ViewBag.AcademicLeaveCount = students.Count(x => x.StudentState == StudentState.Академ);

                var debtRecords = disciplineStudentRecords
                    .Where(x => x.MarkType == MarkType.Неудовлетворительно)
                    .ToList();

                var studentsById = students.ToDictionary(x => x.Id);
                var groupsById = studentGroups.ToDictionary(x => x.Id);

                var studentsWithDebts = new HashSet<int>();
                var groupsWithDebts = new HashSet<int>();
                var highRiskDebtStudents = new HashSet<int>();
                var longDebtRecordCount = 0;

                foreach (var record in debtRecords)
                {
                    studentsWithDebts.Add(record.StudentId);

                    if (!studentsById.TryGetValue(record.StudentId, out var student) ||
                        !student.StudentGroupId.HasValue ||
                        !groupsById.TryGetValue(student.StudentGroupId.Value, out var group))
                    {
                        continue;
                    }

                    groupsWithDebts.Add(group.Id);

                    if (IsHighRiskDebt(group.Course, record.Semester))
                    {
                        highRiskDebtStudents.Add(student.Id);
                        longDebtRecordCount++;
                    }
                }

                ViewBag.DebtStudentCount = studentsWithDebts.Count;
                ViewBag.GroupsWithDebtsCount = groupsWithDebts.Count;
                ViewBag.HighRiskDebtStudentCount = highRiskDebtStudents.Count;
                ViewBag.LongDebtRecordCount = longDebtRecordCount;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                ViewBag.EducationDirectionCount = 0;
                ViewBag.LecturerCount = 0;
                ViewBag.StudentCount = 0;
                ViewBag.StudentGroupCount = 0;
                ViewBag.DisciplineCount = 0;
                ViewBag.AcademicPlanCount = 0;
                ViewBag.StudentOrderCount = 0;
                ViewBag.ClassroomCount = 0;

                ViewBag.DisciplineStudentRecordCount = 0;
                ViewBag.UnsatisfactoryCount = 0;
                ViewBag.AbsentCount = 0;
                ViewBag.AcademicLeaveCount = 0;

                ViewBag.DebtStudentCount = 0;
                ViewBag.GroupsWithDebtsCount = 0;
                ViewBag.HighRiskDebtStudentCount = 0;
                ViewBag.LongDebtRecordCount = 0;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuickSync()
        {
            try
            {
                APIClient.PostRequest("api/core/Sync/academic-plans");
                APIClient.PostRequest("api/core/Sync/student-groups");
                APIClient.PostRequest("api/core/Sync/students");
                APIClient.PostRequest("api/core/Sync/discipline-student-records");
                APIClient.PostRequest("api/core/Sync/student-orders");

                TempData["Success"] = "Быстрая синхронизация выполнена успешно.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private static int GetMaxSemesterForCourse(AcademicCourse course) => course switch
        {
            AcademicCourse.Course_1 => 2,
            AcademicCourse.Course_2 => 4,
            AcademicCourse.Course_3 => 6,
            AcademicCourse.Course_4 => 8,
            _ => 0
        };

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
    }
}