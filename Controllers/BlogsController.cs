using Microsoft.AspNetCore.Mvc;
using LoginAPI.Data;
using LoginAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public BlogsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blogs>>> GetBlogs(int limit, int offset, string query)
        {
            var productsQuery = _context.Blogs.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(query) || p.Description.Contains(query));
            }
            return productsQuery
                .OrderByDescending(p => p.Id)
                .Skip(offset)
                .Take(limit)
                .ToList();
            //return await blogs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs>> GetBlog(int id)
        {
            var blogs = await _context.Blogs.FindAsync(id);

            if (blogs == null)
            {
                return NotFound();
            }

            return blogs;
        }

        [HttpPost]
        public async Task<ActionResult<Blogs>> CreateBlog([FromBody] Blogs newBlog)
        {
            if (newBlog == null)
            {
                return BadRequest("Blog data is null.");
            }
            bool isExist = await _context.Blogs.AnyAsync(e => e.Name == newBlog.Name);
            if(isExist)
            {
                return BadRequest("A blog with the same name exist");
            }

            _context.Blogs.Add(newBlog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlogs), new { id = newBlog.Id }, newBlog);
        }
    }
}