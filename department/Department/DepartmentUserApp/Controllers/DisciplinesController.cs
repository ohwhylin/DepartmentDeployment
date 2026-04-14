using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class DisciplinesController : Controller
    {
        [HttpGet]
        public IActionResult List()
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
    }
}
