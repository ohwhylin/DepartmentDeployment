using Microsoft.AspNetCore.Mvc;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;

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
            }

            return View();
        }
    }
}