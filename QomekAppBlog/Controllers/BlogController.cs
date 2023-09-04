using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomekAppBlog.Entities;
using QomekData.Entities;

namespace QomekAppBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private BlogDbContext _db;

        public BlogController(BlogDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetBlog/{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _db.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpPost("CreateBlog")]
        public async Task<ActionResult<Blog>> CreateBlog([FromBody]Blog blog)
        {
            _db.Blogs.Add(blog);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
        }

        [HttpPut("UpdateBlog/{id}")]
        public async Task<IActionResult> UpdateBlog(int id,[FromBody] Blog blog)
        {
            blog.Id= id;
            _db.Entry(blog).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!_db.Blogs.Any(b => b.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return Ok();
        }

        [HttpDelete("DeleteBlog/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _db.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            _db.Blogs.Remove(blog);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPost("AddComment/{blogId}")]
        public async Task<IActionResult> AddCommentToBlog(int blogId, [FromBody] Comment comment)
        {
            var blog = await _db.Blogs.FirstOrDefaultAsync(i => i.Id == blogId);
            blog.Comments.Add(comment);
            await _db.SaveChangesAsync();
            return Ok();
        }
        
    }
}
