using Dunware.Blog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dunware.Blog.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly BlogContext _context;

        public PostController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var posts = await _context.Post.Where(p => p.Estado == true).ToListAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los posts: {ex.Message}");
            }
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug)
        {
            var post = await _context.Post.FirstOrDefaultAsync(p => p.Slug == slug);
            return Ok(post);
        }
    }

}
