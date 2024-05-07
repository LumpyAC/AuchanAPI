using AuchanAPI.Data;
using AuchanAPI.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuchanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleContext _context;

        public ArticlesController(ArticleContext context)
        {
            _context = context;
        }

        // GET: api/articles
        [HttpGet]
        public async Task<ActionResult<PagedResponse<Article>>> GetArticles(int pageNumber = 1, int pageSize = 10)
        {
            var totalCount = await _context.Articles.CountAsync();

            var articles = await _context.Articles
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var response = new PagedResponse<Article>
            {
                Data = articles,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return response;
        }

        // GET: api/articles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return article;
        }

        // POST: api/articles
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle([FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, article);
        }

        // PUT: api/articles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Articles.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/articles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
