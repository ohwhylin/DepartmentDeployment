using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class StudentOrderBlocksController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlocksList = new List<StudentOrderBlockViewModel>();
                ViewBag.StudentOrdersList = new List<StudentOrderViewModel>();
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

                var item = APIClient.GetRequest<StudentOrderBlockViewModel>($"api/core/StudentOrderBlocks/GetStudentOrderBlock?id={id}");
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
                ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrdersList = new List<StudentOrderViewModel>();
                ViewBag.EducationDirectionsList = new List<EducationDirectionViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(StudentOrderBlockBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                    ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/StudentOrderBlocks/StudentOrderBlockCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            try
            {
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlocksList = new List<StudentOrderBlockViewModel>();
                ViewBag.StudentOrdersList = new List<StudentOrderViewModel>();
                ViewBag.EducationDirectionsList = new List<EducationDirectionViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(StudentOrderBlockBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                    ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                    ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/StudentOrderBlocks/StudentOrderBlockUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                ViewBag.StudentOrdersList = APIClient.GetRequest<List<StudentOrderViewModel>>("api/core/StudentOrders/GetStudentOrderList");
                ViewBag.EducationDirectionsList = APIClient.GetRequest<List<EducationDirectionViewModel>>("api/core/EducationDirections/GetEducationDirectionList");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            try
            {
                ViewBag.StudentOrderBlocksList = APIClient.GetRequest<List<StudentOrderBlockViewModel>>("api/core/StudentOrderBlocks/GetStudentOrderBlockList");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.StudentOrderBlocksList = new List<StudentOrderBlockViewModel>();
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

                APIClient.PostRequest("api/core/StudentOrderBlocks/StudentOrderBlockDelete", new StudentOrderBlockBindingModel
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
