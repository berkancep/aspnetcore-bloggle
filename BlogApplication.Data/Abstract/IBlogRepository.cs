using BlogApplication.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApplication.Data.Abstract
{
    public interface IBlogRepository
    {
        Blog GetById(int blogId);
        IQueryable<Blog> GetAll();
        void AddBlog(Blog entitiy);
        void UpdateBlog(Blog entitiy);
        void SaveBlog(Blog entitiy);
        void DeleteBlog(int blogId);

    }
}
