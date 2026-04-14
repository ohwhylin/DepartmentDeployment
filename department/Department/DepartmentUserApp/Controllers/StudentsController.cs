using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentsList = new List<StudentViewModel>();
                ViewBag.StudentGroupsList = new List<StudentGroupViewModel>();
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

                var item = APIClient.GetRequest<StudentViewModel>($"api/core/Students/GetStudent?id={id}");
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
        public IActionResult Create(StudentBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/Students/StudentCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentsList = new List<StudentViewModel>();
                ViewBag.StudentGroupsList = new List<StudentGroupViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(StudentBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                    ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/Students/StudentUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            try
            {
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentsList = new List<StudentViewModel>();
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

                APIClient.PostRequest("api/core/Students/StudentDelete", new StudentBindingModel
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
                APIClient.PostRequest("api/core/Sync/students");
                TempData["Success"] = "Синхронизация студентов выполнена успешно.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("List");
        }

    }
}
