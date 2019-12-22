using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApplication.Entitiy
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
