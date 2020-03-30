using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E37SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly DataContext _context;

        public ArticleController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public IEnumerable<Models.Article> GetArticles()
        {
            return _context.Articles;
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticles([FromRoute] string id)
        {

            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpGet("find/{query}")]
        public IEnumerable<Models.Article> FindArticles([FromRoute] string query)
        {
            return _context.Articles.Where(c => c.ArticleNumber.Contains(query) || c.Name.Contains(query));
        }
    }
}