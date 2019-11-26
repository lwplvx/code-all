using System;
using System.Linq;
using System.Threading.Tasks;
using TenantDemo.DB;
using TenantDemo.DB.Entities;

namespace TenantDemo.Repository
{
    public interface IPostRepository
    {
        Task<IQueryable<Post>> GetPosts();
    }

    public class PostRepository : IPostRepository
    {
        TenantDbContext _context;
        public PostRepository(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Post>> GetPosts()
        {
            var blogs = _context.Posts;
            return await Task.FromResult(blogs);
        }
    }
}
