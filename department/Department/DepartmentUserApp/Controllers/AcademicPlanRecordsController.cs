using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class AcademicPlanRecordsController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlanRecordsList = new List<AcademicPlanRecordViewModel>();
                ViewBag.AcademicPlansList = new List<AcademicPlanViewModel>();
                ViewBag.DisciplinesList = new List<DisciplineViewModel>();
                ViewBag.AcademicPlanRecordsList = new List<AcademicPlanRecordViewModel>();
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

                var item = APIClient.GetRequest<AcademicPlanRecordViewModel>($"api/core/AcademicPlanRecords/GetAcademicPlanRecord?id={id}");
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
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlansList = new List<AcademicPlanViewModel>();
                ViewBag.DisciplinesList = new List<DisciplineViewModel>();
                ViewBag.AcademicPlanRecordsList = new List<AcademicPlanRecordViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(AcademicPlanRecordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                    ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                    ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/AcademicPlanRecords/AcademicPlanRecordCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlanRecordsList = new List<AcademicPlanRecordViewModel>();
                ViewBag.AcademicPlansList = new List<AcademicPlanViewModel>();
                ViewBag.DisciplinesList = new List<DisciplineViewModel>();
                ViewBag.AcademicPlanRecordsList = new List<AcademicPlanRecordViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(AcademicPlanRecordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                    ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                    ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                    ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/AcademicPlanRecords/AcademicPlanRecordUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            try
            {
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlanRecordsList = new List<AcademicPlanRecordViewModel>();
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

                APIClient.PostRequest("api/core/AcademicPlanRecords/AcademicPlanRecordDelete", new AcademicPlanRecordBindingModel
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
