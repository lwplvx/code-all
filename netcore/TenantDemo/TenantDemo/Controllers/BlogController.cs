using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TenantDemo.DB;
using TenantDemo.Models;
using TenantDemo.Repository;

namespace TenantDemo.Controllers
{
    public class BlogController : Controller
    {
        readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository) => _blogRepository = blogRepository;

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.GetBlogs();
            //     var dtoBlogs = blogs.ProjectTo<BlogDTO>();
            var blogList = blogs.ToList();
            ViewBag.Name = blogList.First().Name;
            return View();
        }
        public async Task<IActionResult> InitDB([FromServices] TenantDbContext dbContext)
        {
           var res= await Task.FromResult( dbContext.Database.EnsureCreated());

            return Json(res);
        }

    }
}
