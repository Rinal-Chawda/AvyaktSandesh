using AvyaktSandesh.Data;
using AvyaktSandesh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AvyaktSandesh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public MediaController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] int articleId, [FromForm] string type)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            string folder = Path.Combine("media", type.ToLower());
            string path = Path.Combine(_env.ContentRootPath, folder);
            Directory.CreateDirectory(path);

            string filePath = Path.Combine(folder, file.FileName);
            string fullPath = Path.Combine(_env.ContentRootPath, filePath);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var media = new MediaFiles
            {
                ArticleId = articleId,
                Type = type,
                FilePath = "/" + filePath.Replace("\\", "/"),
                Caption = file.FileName
            };

            _context.MediaFiles.Add(media);
            await _context.SaveChangesAsync();

            return Ok(media);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            var media = await _context.MediaFiles.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            // Delete the physical file if it exists
            var fullPath = Path.Combine(_env.ContentRootPath, media.FilePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _context.MediaFiles.Remove(media);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("by-article/{articleId}")]
        public async Task<IActionResult> GetMediaByArticleId(int articleId)
        {
            var mediaFiles = await _context.MediaFiles
                .Where(m => m.ArticleId == articleId)
                .ToListAsync();

            if (mediaFiles == null || mediaFiles.Count == 0)
                return NotFound();

            return Ok(mediaFiles);
        }
    }
}
