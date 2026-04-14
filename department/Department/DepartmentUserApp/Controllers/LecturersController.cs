using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp.Controllers
{
    public class LecturersController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                ViewBag.LecturersList =
                    APIClient.GetRequest<List<LecturerViewModel>>(
                        "api/core/Lecturers/GetLecturerList");

                ViewBag.LecturerStudyPostsList =
                    APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                        "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                ViewBag.LecturerDepartmentPostsList =
                    APIClient.GetRequest<List<LecturerDepartmentPostViewModel>>(
                        "api/core/LecturerDepartmentPosts/GetLecturerDepartmentPostList");

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.LecturersList = new List<LecturerViewModel>();
                ViewBag.LecturerStudyPostsList = new List<LecturerStudyPostViewModel>();
                ViewBag.LecturerDepartmentPostsList = new List<LecturerDepartmentPostViewModel>();
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

                var item = APIClient.GetRequest<LecturerViewModel>(
                    $"api/core/Lecturers/GetLecturer?id={id}");

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
                ViewBag.LecturerStudyPostsList =
                    APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                        "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                ViewBag.LecturerDepartmentPostsList =
                    APIClient.GetRequest<List<LecturerDepartmentPostViewModel>>(
                        "api/core/LecturerDepartmentPosts/GetLecturerDepartmentPostList");

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.LecturerStudyPostsList = new List<LecturerStudyPostViewModel>();
                ViewBag.LecturerDepartmentPostsList = new List<LecturerDepartmentPostViewModel>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(LecturerBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.LecturerStudyPostsList =
                        APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                            "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                    ViewBag.LecturerDepartmentPostsList =
                        APIClient.GetRequest<List<LecturerDepartmentPostViewModel>>(
                            "api/core/LecturerDepartmentPosts/GetLecturerDepartmentPostList");

                    return View(model);
                }

                APIClient.PostRequest("api/core/Lecturers/LecturerCreate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                ViewBag.LecturerStudyPostsList =
                    APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                        "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                ViewBag.LecturerDepartmentPostsList =
                    APIClient.GetRequest<List<LecturerDepartmentPostViewModel>>(
                        "api/core/LecturerDepartmentPosts/GetLecturerDepartmentPostList");

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

                var item = APIClient.GetRequest<LecturerViewModel>(
                    $"api/core/Lecturers/GetLecturer?id={id}");

                if (item == null)
                {
                    TempData["Error"] = "Запись не найдена";
                    return RedirectToAction("List");
                }

                ViewBag.LecturerStudyPostsList =
                    APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                        "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                ViewBag.LecturerDepartmentPostsList =
                    APIClient.GetRequest<List<LecturerDepartmentPostViewModel>>(
                        "api/core/LecturerDepartmentPosts/GetLecturerDepartmentPostList");

                var model = new LecturerBindingModel
                {
                    Id = item.Id,
                    LecturerStudyPostId = item.LecturerStudyPostId,
                    LecturerDepartmentPostId = item.LecturerDepartmentPostId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Patronymic = item.Patronymic,
                    Abbreviation = item.Abbreviation,
                    DateBirth = item.DateBirth,
                    Address = item.Address,
                    Email = item.Email,
                    MobileNumber = item.MobileNumber,
                    HomeNumber = item.HomeNumber,
                    Rank = item.Rank,
                    Rank2 = item.Rank2,
                    Description = item.Description,
                    Photo = item.Photo,
                    OnlyForPrivate = item.OnlyForPrivate
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
        public IActionResult Update(LecturerBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.LecturerStudyPostsList =
                        APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                            "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                    ViewBag.LecturerDepartmentPostsList =
                        APIClient.GetRequest<List<LecturerDepartmentPostViewModel>>(
                            "api/core/LecturerDepartmentPosts/GetLecturerDepartmentPostList");

                    return View(model);
                }

                APIClient.PostRequest("api/core/Lecturers/LecturerUpdate", model);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                ViewBag.LecturerStudyPostsList =
                    APIClient.GetRequest<List<LecturerStudyPostViewModel>>(
                        "api/core/LecturerStudyPosts/GetLecturerStudyPostList");

                ViewBag.LecturerDepartmentPostsList =
                    APIClient.GetRequest<List<LecturerDepartmentPostViewModel>>(
                        "api/core/LecturerDepartmentPosts/GetLecturerDepartmentPostList");

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            try
            {
                var list = APIClient.GetRequest<List<LecturerViewModel>>(
                    "api/core/Lecturers/GetLecturerList");

                ViewBag.LecturersList = list ?? new List<LecturerViewModel>();

                if (id.HasValue && id.Value > 0)
                {
                    ViewBag.SelectedId = id.Value;
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.LecturersList = new List<LecturerViewModel>();
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
                    "api/core/Lecturers/LecturerDelete",
                    new LecturerBindingModel
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