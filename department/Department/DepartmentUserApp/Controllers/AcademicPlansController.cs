using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class AcademicPlansController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                ViewBag.AcademicPlanRecordsList = APIClient.GetRequest<List<AcademicPlanRecordViewModel>>("api/core/AcademicPlanRecords/GetAcademicPlanRecordList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlansList = new List<AcademicPlanViewModel>();
                ViewBag.AcademicPlanRecordsList = new List<AcademicPlanRecordViewModel>();
                ViewBag.EducationDirectionsList = new List<EducationDirectionViewModel>();
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

                var item = APIClient.GetRequest<AcademicPlanViewModel>($"api/core/AcademicPlans/GetAcademicPlan?id={id}");
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
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.EducationDirectionsList = new List<EducationDirectionViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(AcademicPlanBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/AcademicPlans/AcademicPlanCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlansList = new List<AcademicPlanViewModel>();
                ViewBag.EducationDirectionsList = new List<EducationDirectionViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(AcademicPlanBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                    ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/AcademicPlans/AcademicPlanUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            try
            {
                ViewBag.AcademicPlansList = APIClient.GetRequest<List<AcademicPlanViewModel>>("api/core/AcademicPlans/GetAcademicPlanList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.AcademicPlansList = new List<AcademicPlanViewModel>();
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

                APIClient.PostRequest("api/core/AcademicPlans/AcademicPlanDelete", new AcademicPlanBindingModel
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
                APIClient.PostRequest("api/core/Sync/academic-plans");
                TempData["Success"] = "Синхронизация учебных планов выполнена успешно.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("List");
        }
    }
}
