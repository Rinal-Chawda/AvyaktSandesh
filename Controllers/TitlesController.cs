using AvyaktSandesh.Data;
using AvyaktSandesh.Models;
using AvyaktSandesh.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvyaktSandesh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TitlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TitlesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTitles([FromQuery] string? language)
        {
            var query = _context.Titles.Include(t => t.Articles)
              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(language))
                query = query.Where(a => a.Language == language);

            var titles = await query
              .Select(a => new
              {
                 a.Id,
                 a.Title,
                 a.Language,
                 Articlecount = a.Articles.Count,
              })
              .ToListAsync();

            return Ok(titles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTitle(int id)
        {
            var title = await _context.Titles.Include(t => t.Articles).FirstOrDefaultAsync(t => t.Id == id);
            if (title == null)
                return NotFound();
            return Ok(title);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTitle([FromBody] CreateTitleDto dto)
        {
            var title = new Titles
            {
                Title = dto.Title,
                Language = dto.Language
                // CreatedAt and Id will be set automatically
            };
            _context.Titles.Add(title);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTitle), new { id = title.Id }, title);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTitle(int id, [FromBody] CreateTitleDto dto)
        //{
        //    var title = await _context.Titles.FindAsync(id);
        //    if (title == null)
        //        return NotFound();

        //    title.Title = dto.Title;
        //    // Do not update CreatedAt to preserve original creation time

        //    _context.Entry(title).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTitle(int id)
        //{
        //    var title = await _context.Titles.FindAsync(id);
        //    if (title == null)
        //        return NotFound();

        //    _context.Titles.Remove(title);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}