using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;
using DepartmentUserApp.ViewModels.StudentOrders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DepartmentUserApp.Controllers
{
    public class StudentOrdersController : Controller
    {
        [HttpGet]
        public IActionResult List(
    string? studentSearch,
    string? groupSearch,
    StudentOrderType? orderType,
    int page = 1,
    int pageSize = 10)
        {
            try
            {
                var studentOrders =
                    APIClient.GetRequest<List<StudentOrderViewModel>>(
                        "api/core/StudentOrders/GetStudentOrderList")
                    ?? new List<StudentOrderViewModel>();

                var studentOrderBlocks =
                    APIClient.GetRequest<List<StudentOrderBlockViewModel>>(
                        "api/core/StudentOrderBlocks/GetStudentOrderBlockList")
                    ?? new List<StudentOrderBlockViewModel>();

                var blockStudents =
                    APIClient.GetRequest<List<StudentOrderBlockStudentViewModel>>(
                        "api/core/StudentOrderBlockStudents/GetStudentOrderBlockStudentList")
                    ?? new List<StudentOrderBlockStudentViewModel>();

                var normalizedStudentSearch = studentSearch?.Trim();
                var normalizedGroupSearch = groupSearch?.Trim();

                var items = studentOrders
                    .OrderByDescending(x => x.OrderDate)
                    .ThenByDescending(x => x.Id)
                    .Select(order =>
                    {
                        var blocks = studentOrderBlocks
                            .Where(x => x.StudentOrderId == order.Id)
                            .OrderBy(x => x.Id)
                            .Select(block =>
                            {
                                var currentStudents = blockStudents
                                    .Where(x => x.StudentOrderBlockId == block.Id)
                                    .Where(x =>
                                        string.IsNullOrWhiteSpace(normalizedStudentSearch) ||
                                        ContainsIgnoreCase(x.Student, normalizedStudentSearch))
                                    .Where(x =>
                                        string.IsNullOrWhiteSpace(normalizedGroupSearch) ||
                                        ContainsIgnoreCase(
                                            $"{x.StudentGroupFrom ?? string.Empty} {x.StudentGroupTo ?? string.Empty}",
                                            normalizedGroupSearch))
                                    .ToList();

                                return new StudentOrderBlockListItemViewModel
                                {
                                    Block = block,
                                    Students = currentStudents
                                };
                            })
                            .Where(x =>
                                x.Students.Any() ||
                                (string.IsNullOrWhiteSpace(normalizedStudentSearch) &&
                                 string.IsNullOrWhiteSpace(normalizedGroupSearch)))
                            .ToList();

                        return new StudentOrderListItemViewModel
                        {
                            Order = order,
                            Blocks = blocks
                        };
                    })
                    .Where(x =>
                        (!orderType.HasValue || x.Order.StudentOrderType == orderType.Value) &&
                        ((string.IsNullOrWhiteSpace(normalizedStudentSearch) &&
                          string.IsNullOrWhiteSpace(normalizedGroupSearch))
                            ? true
                            : x.Blocks.Any()))
                    .ToList();

                var model = new StudentOrderListPageViewModel
                {
                    StudentSearch = normalizedStudentSearch,
                    GroupSearch = normalizedGroupSearch,
                    OrderType = orderType,
                    OrderTypes = studentOrders
                        .Select(x => x.StudentOrderType)
                        .Distinct()
                        .OrderBy(x => (int)x)
                        .ToList(),
                    Result = PagedResult<StudentOrderListItemViewModel>.Create(items, page, pageSize)
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                var model = new StudentOrderListPageViewModel
                {
                    StudentSearch = studentSearch?.Trim(),
                    GroupSearch = groupSearch?.Trim(),
                    OrderType = orderType,
                    Result = PagedResult<StudentOrderListItemViewModel>.Create(
                        new List<StudentOrderListItemViewModel>(),
                        page,
                        pageSize)
                };

                return View(model);
            }
        }

        private static bool ContainsIgnoreCase(string? source, string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return true;

            if (string.IsNullOrWhiteSpace(source))
                return false;

            return source.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase);
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

                var item = APIClient.GetRequest<StudentOrderViewModel>($"api/core/StudentOrders/GetStudentOrder?id={id}");
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
        public IActionResult Create(StudentOrderBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                APIClient.PostRequest("api/core/StudentOrders/StudentOrderCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrdersList = new List<StudentOrderViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(StudentOrderBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/StudentOrders/StudentOrderUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            try
            {
                ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrdersList = new List<StudentOrderViewModel>();
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

                APIClient.PostRequest("api/core/StudentOrders/StudentOrderDelete", new StudentOrderBindingModel
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
                APIClient.PostRequest("api/core/Sync/student-orders");
                TempData["Success"] = "Синхронизация приказов выполнена успешно.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("List");
        }

    }
}
