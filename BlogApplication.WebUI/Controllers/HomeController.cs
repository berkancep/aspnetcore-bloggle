using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Data.Abstract;
using BlogApplication.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        public HomeController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            HomeBlogModel model = new HomeBlogModel();

            model.HomeBlogs = _blogRepository.GetAll().Where(a => a.IsApproved && a.IsHome).ToList();
            model.SliderBlogs = _blogRepository.GetAll().Where(a => a.IsApproved && a.IsSlider).ToList();

            return View(model);
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}