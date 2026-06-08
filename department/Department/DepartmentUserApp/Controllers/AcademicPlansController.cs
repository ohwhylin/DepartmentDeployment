using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;
using DepartmentUserApp.ViewModels.AcademicPlans;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DepartmentUserApp.Controllers
{
    public class AcademicPlansController : Controller
    {
        [HttpGet]
        public IActionResult List(
    AcademicCourse? course,
    string? year,
    EducationDirectionQualification? qualification,
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

                var queryParts = new List<string>();

                if (course.HasValue)
                {
                    queryParts.Add($"AcademicCourses={(int)course.Value}");
                }

                if (!string.IsNullOrWhiteSpace(year))
                {
                    queryParts.Add($"Year={Uri.EscapeDataString(year.Trim())}");
                }

                if (qualification.HasValue)
                {
                    queryParts.Add($"Qualification={(int)qualification.Value}");
                }

                queryParts.Add($"Page={page}");
                queryParts.Add($"PageSize={pageSize}");

                var plansUrl =
                    $"api/core/AcademicPlans/GetAcademicPlanPage?{string.Join("&", queryParts)}";

                var pagedPlans =
                    APIClient.GetRequest<PagedResult<AcademicPlanViewModel>>(plansUrl)
                    ?? new PagedResult<AcademicPlanViewModel>
                    {
                        Page = 1,
                        PageSize = pageSize,
                        TotalCount = 0
                    };

                var records =
                    APIClient.GetRequest<List<AcademicPlanRecordViewModel>>(
                        "api/core/AcademicPlanRecords/GetAcademicPlanRecordList")
                    ?? new List<AcademicPlanRecordViewModel>();

                var model = new AcademicPlanListPageViewModel
                {
                    Course = course,
                    Year = year?.Trim() ?? string.Empty,
                    Qualification = qualification,
                    Result = pagedPlans,
                    Records = records
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return View(new AcademicPlanListPageViewModel
                {
                    Course = course,
                    Year = year?.Trim() ?? string.Empty,
                    Qualification = qualification,
                    Result = new PagedResult<AcademicPlanViewModel>
                    {
                        Page = 1,
                        PageSize = pageSize,
                        TotalCount = 0
                    },
                    Records = new List<AcademicPlanRecordViewModel>()
                });
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
