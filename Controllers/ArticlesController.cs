﻿using AvyaktSandesh.Data;
using AvyaktSandesh.Models;
using AvyaktSandesh.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

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

        [HttpGet("filter")]
        public async Task<IActionResult> FilterArticles([FromQuery] string? language, [FromQuery] string? title, [FromQuery] string? articleTitle, [FromQuery] int? year)
        {
            var query = _context.Articles
                .Include(a => a.Title)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(language))
                query = query.Where(a => a.Language == language);

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(a => EF.Functions.Like(a.Title.Title, $"%{title}%"));

            if (!string.IsNullOrWhiteSpace(articleTitle))
                query = query.Where(a => EF.Functions.Like(a.ArticleTitle, $"%{articleTitle}%"));


            if (year.HasValue)
                query = query.Where(a => a.ArticleDate.Year == year.Value);

            var articles = await query
               .Select(a => new
               {
                   a.Id,
                   a.ArticleTitle,
                   a.Body,
                   a.ArticleDate,
                   a.Language,
                   a.TitleId,
                   Title = a.Title.Title
               })
               .ToListAsync();

            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDto dto)
        {
            var article = new Articles
            {
                ArticleTitle = dto.ArticleTitle,
                Body = dto.Body,
                ArticleDate = dto.ArticleDate,
                Language = dto.Language,
                TitleId = dto.TitleId
            };
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetArticles), new { id = article.Id }, article);
        }


        [HttpGet("years")]
        public async Task<IActionResult> GetArticlesByYear()
        {
            var articles = await _context.Articles.GroupBy(t => t.ArticleDate.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    count = g.Count()
                })
              .ToListAsync();


            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _context.Articles
                .Where(a => a.Id == id)
                .Select(a => new
                {
                    a.Id,
                    a.ArticleTitle,
                    a.Body,
                    a.ArticleDate,
                    mediaFiles = a.MediaFiles.Select(m => new
                    {
                        id = m.Id,
                        type = m.Type,
                        filePath = m.FilePath,
                        caption = m.Caption,
                    }).ToList()

                }).FirstOrDefaultAsync();

            if (article == null)
                return NotFound();

            return Ok(article);
        }
    }
    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateArticle(int id, Articles updatedArticle)
    //{
    //    if (id != updatedArticle.Id)
    //    {
    //        return BadRequest("Article ID mismatch.");
    //    }

    //    var article = await _context.Articles.Include(a => a.MediaFiles).FirstOrDefaultAsync(a => a.Id == id);
    //    if (article == null)
    //    {
    //        return NotFound();
    //    }

    //    // Update fields
    //    article.ArticleTitle = updatedArticle.ArticleTitle;
    //    article.Body = updatedArticle.Body;
    //    article.TitleId = updatedArticle.TitleId;
    //    // Do not update CreatedAt to preserve original creation time

    //    // Optionally update MediaFiles if needed (not shown here)

    //    _context.Entry(article).State = EntityState.Modified;
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

}
