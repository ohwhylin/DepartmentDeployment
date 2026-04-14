using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class DisciplineBlocksController : Controller
    {
        [HttpGet]
        public IActionResult List()
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

                var item = APIClient.GetRequest<DisciplineBlockViewModel>($"api/core/DisciplineBlocks/GetDisciplineBlock?id={id}");
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
        public IActionResult Create(DisciplineBlockBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                APIClient.PostRequest("api/core/DisciplineBlocks/DisciplineBlockCreate", model);
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
        public IActionResult Update(DisciplineBlockBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DisciplineBlocksList = APIClient.GetRequest<List<DisciplineBlockViewModel>>("api/core/DisciplineBlocks/GetDisciplineBlockList");
                    return View(model);
                }

                APIClient.PostRequest("api/core/DisciplineBlocks/DisciplineBlockUpdate", model);
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
        public IActionResult Delete()
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
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Некорректный идентификатор";
                    return RedirectToAction("Delete");
                }

                APIClient.PostRequest("api/core/DisciplineBlocks/DisciplineBlockDelete", new DisciplineBlockBindingModel
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
