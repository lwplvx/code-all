using System;
using System.Linq;
using System.Threading.Tasks;
using TenantDemo.DB;
using TenantDemo.DB.Entities;

namespace TenantDemo.Repository
{
    public interface IBlogRepository
    {
        Task<IQueryable<Blog>> GetBlogs();
    }

    public class BlogRepository:IBlogRepository
    {
        TenantDbContext _context;
        public BlogRepository(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Blog>> GetBlogs()
        {
            var blogs = _context.Blogs;
            return await Task.FromResult(blogs);
        }
    }
}
