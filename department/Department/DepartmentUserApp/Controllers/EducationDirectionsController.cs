using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;
using DepartmentUserApp.ViewModels.EducationDirections;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DepartmentUserApp.Controllers
{
    public class EducationDirectionsController : Controller
    {
        [HttpGet]
        public IActionResult List(string? search, EducationDirectionQualification? qualification, int page = 1, int pageSize = 10)
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

                if (!string.IsNullOrWhiteSpace(search))
                {
                    queryParts.Add($"Search={Uri.EscapeDataString(search.Trim())}");
                }

                if (qualification.HasValue)
                {
                    queryParts.Add($"Qualification={(int)qualification.Value}");
                }

                queryParts.Add($"Page={page}");
                queryParts.Add($"PageSize={pageSize}");

                var requestUrl =
                    $"api/core/EducationDirections/GetEducationDirectionPage?{string.Join("&", queryParts)}";

                var result =
                    APIClient.GetRequest<PagedResult<EducationDirectionViewModel>>(requestUrl)
                    ?? new PagedResult<EducationDirectionViewModel>
                    {
                        Page = 1,
                        PageSize = pageSize,
                        TotalCount = 0
                    };

                var model = new EducationDirectionListPageViewModel
                {
                    Search = search?.Trim() ?? string.Empty,
                    Qualification = qualification,
                    Result = result
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return View(new EducationDirectionListPageViewModel
                {
                    Search = search?.Trim() ?? string.Empty,
                    Qualification = qualification,
                    Result = new PagedResult<EducationDirectionViewModel>
                    {
                        Page = 1,
                        PageSize = pageSize,
                        TotalCount = 0
                    }
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

                var item = APIClient.GetRequest<EducationDirectionViewModel>(
                    $"api/core/EducationDirections/GetEducationDirection?id={id}");

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
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(EducationDirectionBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                APIClient.PostRequest("api/core/EducationDirections/EducationDirectionCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Некорректный идентификатор";
                    return RedirectToAction("List");
                }

                var item = APIClient.GetRequest<EducationDirectionViewModel>(
                    $"api/core/EducationDirections/GetEducationDirection?id={id}");

                if (item == null)
                {
                    TempData["Error"] = "Запись не найдена";
                    return RedirectToAction("List");
                }

                var model = new EducationDirectionBindingModel
                {
                    Id = item.Id,
                    Cipher = item.Cipher,
                    ShortName = item.ShortName,
                    Title = item.Title,
                    Profile = item.Profile,
                    Qualification = item.Qualification,
                    Description = item.Description
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult Update(EducationDirectionBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                APIClient.PostRequest("api/core/EducationDirections/EducationDirectionUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            try
            {
                var list = APIClient.GetRequest<List<EducationDirectionViewModel>>(
                    "api/core/EducationDirections/GetEducationDirectionList");

                ViewBag.EducationDirectionsList = list ?? new List<EducationDirectionViewModel>();

                if (id.HasValue && id.Value > 0)
                {
                    ViewBag.SelectedId = id.Value;
                }

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
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Некорректный идентификатор";
                    return RedirectToAction("Delete");
                }

                APIClient.PostRequest("api/core/EducationDirections/EducationDirectionDelete",
                    new EducationDirectionBindingModel
                    {
                        Id = id
                    });

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Delete", new { id });
            }
        }
    }
}


