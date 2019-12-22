using BlogApplication.Data.Abstract;
using BlogApplication.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApplication.Data.Concrete.EntitiyFramework
{
    public class EfBlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;
        public EfBlogRepository(BlogContext context)
        {
            _context = context;
        }

        public void AddBlog(Blog entitiy)
        {
            entitiy.Date = DateTime.Now;
            _context.Blogs.Add(entitiy);
            _context.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var blog = GetById(blogId);

            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
            }
        }

        public IQueryable<Blog> GetAll()
        {
            return _context.Blogs;
        }

        public Blog GetById(int blogId)
        {
            return _context.Blogs.FirstOrDefault(a => a.Id == blogId);
        }

        public void SaveBlog(Blog entitiy)
        {
           
            if (entitiy.Id == 0)
            {
                entitiy.Date = DateTime.Now;
                _context.Blogs.Add(entitiy);
            }
            else
            {
                var blog = GetById(entitiy.Id);

                if (blog != null)
                {
                    blog.Title = entitiy.Title;
                    blog.Description = entitiy.Description;
                    blog.Body = entitiy.Body;
                    blog.CategoryId = entitiy.CategoryId;
                    blog.Image = entitiy.Image;
                    blog.IsApproved = entitiy.IsApproved;
                    blog.IsHome = entitiy.IsHome;
                    blog.IsSlider = entitiy.IsSlider;


                }
            }
            _context.SaveChanges();
        }

        public void UpdateBlog(Blog entitiy)
        {
            var blog = GetById(entitiy.Id);

            if (blog != null)
            {
                blog.Title = entitiy.Title;
                blog.Description = entitiy.Description;
                blog.Body = entitiy.Body;
                blog.CategoryId = entitiy.CategoryId;
                blog.Image = entitiy.Image;
                blog.IsApproved = entitiy.IsApproved;
                blog.IsHome = entitiy.IsHome;
                blog.IsSlider = entitiy.IsSlider;

                _context.SaveChanges();
            }
        }
    }
}
