using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Data.Abstract;
using BlogApplication.Entitiy;
using BlogApplication.WebUI.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApplication.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(int? id, string q)
        {
            var blogs = _blogRepository.GetAll().Where(a => a.IsApproved);

            if (!string.IsNullOrWhiteSpace(q))
            {
                blogs = blogs.Where(a => a.Title.Contains(q) || a.Description.Contains(q) || a.Body.Contains(q));
            }


            if (id != null)
            {
                blogs = blogs.Where(a => a.CategoryId == id);
            }


            return View(blogs.OrderByDescending(a => a.Date));

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_blogRepository.GetById(id));
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(_blogRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Blog entitiy)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.AddBlog(entitiy);
                TempData["message"] = Messages.BlogCreated;
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(entitiy);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(_blogRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blog entitiy, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", Image.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);

                        entitiy.Image = Image.FileName;
                    }                 
                }

                _blogRepository.UpdateBlog(entitiy);
                TempData["message"] = Messages.BlogUpdated;
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(entitiy);
        } // fotoğraf için IFormFile alıyoruz

        [HttpGet]
        public IActionResult CreateOrEdit(int? id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");

            if (id == null)
            {
                return View(new Blog());
            }
            else
            {
                return View(_blogRepository.GetById((int)id));
            }
        }

        [HttpPost]
        public IActionResult CreateOrEdit(Blog entitiy)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.SaveBlog(entitiy);
                TempData["message"] = Messages.OperationSuccess;
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(entitiy);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_blogRepository.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _blogRepository.DeleteBlog(id);

            TempData["message"] = Messages.BlogDeleted;
            return RedirectToAction("List");
        }
    }
}