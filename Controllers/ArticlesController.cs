using AvyaktSandesh.Data;
using AvyaktSandesh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvyaktSandesh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArticlesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _context.Articles.Include(a => a.MediaFiles).ToListAsync();
            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(Articles article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetArticles), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, Articles updatedArticle)
        {
            if (id != updatedArticle.Id)
            {
                return BadRequest("Article ID mismatch.");
            }

            var article = await _context.Articles.Include(a => a.MediaFiles).FirstOrDefaultAsync(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            // Update fields
            article.ArticleTitle = updatedArticle.ArticleTitle;
            article.Body = updatedArticle.Body;
            article.TitleId = updatedArticle.TitleId;
            // Do not update CreatedAt to preserve original creation time

            // Optionally update MediaFiles if needed (not shown here)

            _context.Entry(article).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
