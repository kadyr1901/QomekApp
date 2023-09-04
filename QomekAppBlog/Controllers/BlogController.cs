using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QomekAppBlog;
using QomekCore.Repository;
using QomekData;
using QomekData.Entities;

namespace QomekAppBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogService;

        public BlogController(QomekDbContext db,IBlogRepository blogRepository)
        {
            _blogService = blogRepository;
        }

        [HttpGet("GetBlog/{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _blogService.Get(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpPost("CreateBlog")]
        public async Task<ActionResult<Blog>> CreateBlog([FromBody]Blog blog)
        {
            return await _blogService.AddAsync(blog);
        }

        [HttpPut("UpdateBlog/{id}")]
        public async Task<IActionResult> UpdateBlog(int id,[FromBody] Blog blog)
        {
            try
            {
                _blogService.Update(blog);
            }
            catch (Exception)
            {
                if (await _blogService.Get(id)==null)
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
            await _blogService.DeleteAsync(id);

            return NoContent();
        }
        
        [HttpPost("AddComment/{blogId}")]
        public async Task<IActionResult> AddCommentsToBlog(int blogId, [FromBody] List<Comment> comments)
        {
            var blog=await _blogService.AddComments(comments,blogId);
            if(blog != null)
                return Ok();
            else
                return NotFound();
        }
        
    }
}
