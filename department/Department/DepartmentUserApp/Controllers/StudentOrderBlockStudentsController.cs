using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class StudentOrderBlockStudentsController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.StudentOrderBlockStudentsList = APIClient.GetRequest<List<StudentOrderBlockStudentViewModel>>("api/core/StudentOrderBlockStudents/GetStudentOrderBlockStudentList");
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlockStudentsList = new List<StudentOrderBlockStudentViewModel>();
                ViewBag.StudentOrderBlocksList = new List<StudentOrderBlockViewModel>();
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

                var item = APIClient.GetRequest<StudentOrderBlockStudentViewModel>($"api/core/StudentOrderBlockStudents/GetStudentOrderBlockStudent?id={id}");
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
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlocksList = new List<StudentOrderBlockViewModel>();
                ViewBag.StudentsList = new List<StudentViewModel>();
                ViewBag.StudentGroupsList = new List<StudentGroupViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(StudentOrderBlockStudentBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                    ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                    ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/StudentOrderBlockStudents/StudentOrderBlockStudentCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.StudentOrderBlockStudentsList = APIClient.GetRequest<List<StudentOrderBlockStudentViewModel>>("api/core/StudentOrderBlockStudents/GetStudentOrderBlockStudentList");
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlockStudentsList = new List<StudentOrderBlockStudentViewModel>();
                ViewBag.StudentOrderBlocksList = new List<StudentOrderBlockViewModel>();
                ViewBag.StudentsList = new List<StudentViewModel>();
                ViewBag.StudentGroupsList = new List<StudentGroupViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(StudentOrderBlockStudentBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StudentOrderBlockStudentsList = APIClient.GetRequest<List<StudentOrderBlockStudentViewModel>>("api/core/StudentOrderBlockStudents/GetStudentOrderBlockStudentList");
                    ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                    ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                    ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/StudentOrderBlockStudents/StudentOrderBlockStudentUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlockStudentsList = APIClient.GetRequest<List<StudentOrderBlockStudentViewModel>>("api/core/StudentOrderBlockStudents/GetStudentOrderBlockStudentList");
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
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
                ViewBag.StudentOrderBlockStudentsList = APIClient.GetRequest<List<StudentOrderBlockStudentViewModel>>("api/core/StudentOrderBlockStudents/GetStudentOrderBlockStudentList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlockStudentsList = new List<StudentOrderBlockStudentViewModel>();
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

                APIClient.PostRequest("api/core/StudentOrderBlockStudents/StudentOrderBlockStudentDelete", new StudentOrderBlockStudentBindingModel
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
    }
}
