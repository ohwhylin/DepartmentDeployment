using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class StudentGroupsController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                ViewBag.LecturersList = APIClient.GetRequest<List<LecturerViewModel>>("api/core/Lecturers/GetLecturerList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentGroupsList = new List<StudentGroupViewModel>();
                ViewBag.StudentsList = new List<StudentViewModel>();
                ViewBag.EducationDirectionsList = new List<EducationDirectionViewModel>();
                ViewBag.LecturersList = new List<LecturerViewModel>();
                return View();
            }
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
