using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Data.Abstract;
using BlogApplication.Entitiy;
using BlogApplication.WebUI.Constants;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(_categoryRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category entitiy)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddCategory(entitiy);
                TempData["message"] = Messages.CategoryCreated;
                return RedirectToAction("List");
            }

            return View(entitiy);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_categoryRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Category entitiy)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(entitiy);
                TempData["message"] = Messages.CategoryUpdated;
                return RedirectToAction("List");
            }

            return View(entitiy);

        }

        [HttpGet]
        public IActionResult CreateOrEdit(int? id)
        {
            if (id == null)
            {
                return View(new Category());
            }
            else
            {
                return View(_categoryRepository.GetById((int)id));
            }
        }

        [HttpPost]
        public IActionResult CreateOrEdit(Category entitiy)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.SaveCategory(entitiy);
                TempData["message"] = Messages.OperationSuccess;
                return RedirectToAction("List");
            }

            return View(entitiy);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_categoryRepository.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            TempData["message"] = Messages.CategoryDeleted;
            _categoryRepository.DeleteCategory(id);
            return RedirectToAction("List");
        }
    }
}