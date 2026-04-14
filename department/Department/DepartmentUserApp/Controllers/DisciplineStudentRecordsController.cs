using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class DisciplineStudentRecordsController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.DisciplineStudentRecordsList = APIClient.GetRequest<List<DisciplineStudentRecordViewModel>>("api/core/DisciplineStudentRecords/GetDisciplineStudentRecordList");
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                ViewBag.StudentGroupsList = APIClient.GetRequest<List<StudentGroupViewModel>>("api/core/StudentGroups/GetStudentGroupList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplineStudentRecordsList = new List<DisciplineStudentRecordViewModel>();
                ViewBag.DisciplinesList = new List<DisciplineViewModel>();
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

                var item = APIClient.GetRequest<DisciplineStudentRecordViewModel>($"api/core/DisciplineStudentRecords/GetDisciplineStudentRecord?id={id}");
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
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplinesList = new List<DisciplineViewModel>();
                ViewBag.StudentsList = new List<StudentViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(DisciplineStudentRecordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                    ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/DisciplineStudentRecords/DisciplineStudentRecordCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.DisciplineStudentRecordsList = APIClient.GetRequest<List<DisciplineStudentRecordViewModel>>("api/core/DisciplineStudentRecords/GetDisciplineStudentRecordList");
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplineStudentRecordsList = new List<DisciplineStudentRecordViewModel>();
                ViewBag.DisciplinesList = new List<DisciplineViewModel>();
                ViewBag.StudentsList = new List<StudentViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(DisciplineStudentRecordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DisciplineStudentRecordsList = APIClient.GetRequest<List<DisciplineStudentRecordViewModel>>("api/core/DisciplineStudentRecords/GetDisciplineStudentRecordList");
                    ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                    ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/DisciplineStudentRecords/DisciplineStudentRecordUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplineStudentRecordsList = APIClient.GetRequest<List<DisciplineStudentRecordViewModel>>("api/core/DisciplineStudentRecords/GetDisciplineStudentRecordList");
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.StudentsList = APIClient.GetRequest<List<StudentViewModel>>("api/core/Students/GetStudentList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            try
            {
                ViewBag.DisciplineStudentRecordsList = APIClient.GetRequest<List<DisciplineStudentRecordViewModel>>("api/core/DisciplineStudentRecords/GetDisciplineStudentRecordList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplineStudentRecordsList = new List<DisciplineStudentRecordViewModel>();
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

                APIClient.PostRequest("api/core/DisciplineStudentRecords/DisciplineStudentRecordDelete", new DisciplineStudentRecordBindingModel
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
                APIClient.PostRequest("api/core/Sync/discipline-student-records");
                TempData["Success"] = "Синхронизация успеваемости выполнена успешно.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("List");
        }

    }
}
