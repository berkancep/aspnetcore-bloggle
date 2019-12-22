using BlogApplication.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApplication.Data.Abstract
{
    public interface ICategoryRepository
    {
        Category GetById(int categoryId);
        IQueryable<Category> GetAll();
        void AddCategory(Category entitiy);
        void UpdateCategory(Category entitiy);
        void SaveCategory(Category entitiy);
        void DeleteCategory(int categoryId);
    }
}
