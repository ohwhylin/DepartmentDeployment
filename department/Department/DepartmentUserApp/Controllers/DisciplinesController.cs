using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentUserApp.ViewModels.Disciplines;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DepartmentUserApp.Controllers
{
    public class DisciplinesController : Controller
    {
        [HttpGet]
        public IActionResult List(string? search, int page = 1, int pageSize = 10)
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

                queryParts.Add($"Page={page}");
                queryParts.Add($"PageSize={pageSize}");

                var requestUrl =
                    $"api/core/Disciplines/GetDisciplinePage?{string.Join("&", queryParts)}";

                var result =
                    APIClient.GetRequest<PagedResult<DisciplineViewModel>>(requestUrl)
                    ?? new PagedResult<DisciplineViewModel>
                    {
                        Page = 1,
                        PageSize = pageSize,
                        TotalCount = 0
                    };

                var model = new DisciplineListPageViewModel
                {
                    Search = search?.Trim() ?? string.Empty,
                    Result = result
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return View(new DisciplineListPageViewModel
                {
                    Search = search?.Trim() ?? string.Empty,
                    Result = new PagedResult<DisciplineViewModel>
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

                var item = APIClient.GetRequest<DisciplineViewModel>($"api/core/Disciplines/GetDiscipline?id={id}");
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
                ViewBag.DisciplineBlocksList = APIClient.GetRequest<List<DisciplineBlockViewModel>>("api/core/DisciplineBlocks/GetDisciplineBlockList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplineBlocksList = new List<DisciplineBlockViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(DisciplineBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DisciplineBlocksList = APIClient.GetRequest<List<DisciplineBlockViewModel>>("api/core/DisciplineBlocks/GetDisciplineBlockList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/Disciplines/DisciplineCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplineBlocksList = APIClient.GetRequest<List<DisciplineBlockViewModel>>("api/core/DisciplineBlocks/GetDisciplineBlockList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.DisciplineBlocksList = APIClient.GetRequest<List<DisciplineBlockViewModel>>("api/core/DisciplineBlocks/GetDisciplineBlockList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplinesList = new List<DisciplineViewModel>();
                ViewBag.DisciplineBlocksList = new List<DisciplineBlockViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(DisciplineBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                    ViewBag.DisciplineBlocksList = APIClient.GetRequest<List<DisciplineBlockViewModel>>("api/core/DisciplineBlocks/GetDisciplineBlockList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/Disciplines/DisciplineUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                ViewBag.DisciplineBlocksList = APIClient.GetRequest<List<DisciplineBlockViewModel>>("api/core/DisciplineBlocks/GetDisciplineBlockList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            try
            {
                ViewBag.DisciplinesList = APIClient.GetRequest<List<DisciplineViewModel>>("api/core/Disciplines/GetDisciplineList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.DisciplinesList = new List<DisciplineViewModel>();
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

                APIClient.PostRequest("api/core/Disciplines/DisciplineDelete", new DisciplineBindingModel
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
        [ValidateAntiForgeryToken]
        public IActionResult Sync()
        {
            try
            {
                APIClient.PostRequest("api/core/Sync/academic-plans");
                TempData["Success"] = "Синхронизация дисциплин выполнена успешно.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(List));
        }
    }
}
