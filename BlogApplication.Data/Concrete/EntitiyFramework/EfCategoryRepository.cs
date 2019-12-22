using BlogApplication.Data.Abstract;
using BlogApplication.Entitiy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApplication.Data.Concrete.EntitiyFramework
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private readonly BlogContext _context;
        public EfCategoryRepository(BlogContext context)
        {
            _context = context;
        }

        public void AddCategory(Category entitiy)
        {
            _context.Categories.Add(entitiy);
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = GetById(categoryId);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(a => a.Id == categoryId);
        }

        public void SaveCategory(Category entitiy)
        {
            if (entitiy.Id == 0)
            {
                _context.Categories.Add(entitiy);
            }
            else
            {
                var category = GetById(entitiy.Id);

                if (category != null)
                {
                    category.Name = entitiy.Name;
                }
            }

            _context.SaveChanges();
        }

        public void UpdateCategory(Category entitiy)
        {
            _context.Update(entitiy);
            _context.SaveChanges();
        }
    }
}
