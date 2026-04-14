using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class LecturerStudyPostsController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.LecturerStudyPostsList =
                    APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                        "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.LecturerStudyPostsList = new List<LecturerStudyPostViewModel>();
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

                var item = APIClient.GetRequest<LecturerStudyPostViewModel>(
                    $"api/core/LecturerStudyPosts/GetLecturerStudyPost?id={id}");

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
        public IActionResult Create(LecturerStudyPostBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                APIClient.PostRequest(
                    "api/core/LecturerStudyPosts/LecturerStudyPostCreate",
                    model);

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

                var item = APIClient.GetRequest<LecturerStudyPostViewModel>(
                    $"api/core/LecturerStudyPosts/GetLecturerStudyPost?id={id}");

                if (item == null)
                {
                    TempData["Error"] = "Запись не найдена";
                    return RedirectToAction("List");
                }

                var model = new LecturerStudyPostBindingModel
                {
                    Id = item.Id,
                    StudyPostTitle = item.StudyPostTitle,
                    Hours = item.Hours
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
        public IActionResult Update(LecturerStudyPostBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                APIClient.PostRequest(
                    "api/core/LecturerStudyPosts/LecturerStudyPostUpdate",
                    model);

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
                var list = APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                    "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                ViewBag.LecturerStudyPostsList = list ?? new List<LecturerStudyPostViewModel>();

                if (id.HasValue && id.Value > 0)
                {
                    ViewBag.SelectedId = id.Value;
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.LecturerStudyPostsList = new List<LecturerStudyPostViewModel>();
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
                    "api/core/LecturerStudyPosts/LecturerStudyPostDelete",
                    new LecturerStudyPostBindingModel
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