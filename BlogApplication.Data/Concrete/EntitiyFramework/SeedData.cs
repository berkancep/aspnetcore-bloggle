using BlogApplication.Entitiy;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApplication.Data.Concrete.EntitiyFramework
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            BlogContext context = app.ApplicationServices.GetRequiredService<BlogContext>();

            context.Database.Migrate();


            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category() { Name = "Category 1" },
                    new Category() { Name = "Category 2" },
                    new Category() { Name = "Category 3" }
                    );

                context.SaveChanges();
            }

            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(
                    new Blog() { Title = "Title 1", Description = "Description 1", Body = "Body 1", Image = "1.jpg", Date = DateTime.Now.AddDays(-5), IsApproved = true, CategoryId = 1 },

                    new Blog() { Title = "Title 2", Description = "Description 2", Body = "Body 2", Image = "2.jpg", Date = DateTime.Now.AddDays(-7), IsApproved = true, CategoryId = 1 },

                    new Blog() { Title = "Title 3", Description = "Description 3", Body = "Body 3", Image = "3.jpg", Date = DateTime.Now.AddDays(-8), IsApproved = false, CategoryId = 2 },

                    new Blog() { Title = "Title 4", Description = "Description 4", Body = "Body 4", Image = "4.jpg", Date = DateTime.Now.AddDays(-9), IsApproved = true, CategoryId = 3 }
                    );

                context.SaveChanges();
            }
        }
    }
}
