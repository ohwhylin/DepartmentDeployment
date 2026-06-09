using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;
using DepartmentUserApp.ViewModels.Classrooms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DepartmentUserApp.Controllers
{
    public class ClassroomsController : Controller
    {
        [HttpGet]
        public IActionResult List(
    string? search,
    ClassroomTypes? type,
    bool? hasProjector,
    bool? useInSchedule,
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

                if (!string.IsNullOrWhiteSpace(search))
                {
                    queryParts.Add($"Number={Uri.EscapeDataString(search.Trim())}");
                }

                if (type.HasValue)
                {
                    queryParts.Add($"Type={(int)type.Value}");
                }

                if (hasProjector.HasValue)
                {
                    queryParts.Add($"HasProjector={hasProjector.Value.ToString().ToLowerInvariant()}");
                }

                if (useInSchedule.HasValue)
                {
                    queryParts.Add($"UseInSchedule={useInSchedule.Value.ToString().ToLowerInvariant()}");
                }

                queryParts.Add($"Page={page}");
                queryParts.Add($"PageSize={pageSize}");

                var url = $"api/core/Classrooms/GetClassroomPage?{string.Join("&", queryParts)}";

                var result = APIClient.GetRequest<PagedResult<ClassroomViewModel>>(url)
                    ?? new PagedResult<ClassroomViewModel>
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalCount = 0
                    };

                var model = new ClassroomListPageViewModel
                {
                    Search = search?.Trim() ?? string.Empty,
                    Type = type,
                    HasProjector = hasProjector,
                    UseInSchedule = useInSchedule,
                    Result = result
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return View(new ClassroomListPageViewModel
                {
                    Search = search?.Trim() ?? string.Empty,
                    Type = type,
                    HasProjector = hasProjector,
                    UseInSchedule = useInSchedule,
                    Result = new PagedResult<ClassroomViewModel>
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

                var item = APIClient.GetRequest<ClassroomViewModel>(
                    $"api/core/Classrooms/GetClassroom?id={id}");

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
        public IActionResult Create(ClassroomBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                APIClient.PostRequest("api/core/Classrooms/ClassroomCreate", model);
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

                var item = APIClient.GetRequest<ClassroomViewModel>(
                    $"api/core/Classrooms/GetClassroom?id={id}");

                if (item == null)
                {
                    TempData["Error"] = "Запись не найдена";
                    return RedirectToAction("List");
                }

                var model = new ClassroomBindingModel
                {
                    Id = item.Id,
                    Number = item.Number,
                    Type = item.Type,
                    Capacity = item.Capacity,
                    NotUseInSchedule = item.NotUseInSchedule,
                    HasProjector = item.HasProjector
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
        public IActionResult Update(ClassroomBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                APIClient.PostRequest("api/core/Classrooms/ClassroomUpdate", model);
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
                var list = APIClient.GetRequest<List<ClassroomViewModel>>(
                    "api/core/Classrooms/GetClassroomList");

                ViewBag.ClassroomsList = list ?? new List<ClassroomViewModel>();

                if (id.HasValue && id.Value > 0)
                {
                    ViewBag.SelectedId = id.Value;
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.ClassroomsList = new List<ClassroomViewModel>();
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

                APIClient.PostRequest(
                    "api/core/Classrooms/ClassroomDelete",
                    new ClassroomBindingModel
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