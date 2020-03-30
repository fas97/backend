using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using FortnoxAPILibrary;
using FortnoxAPILibrary.Connectors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E37SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FortnoxController : ControllerBase
    {

        private readonly DataContext _context;

        public FortnoxController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Fortnox/Refresh
        [HttpGet]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                // Ta bort kunder från den lokala databasen
                if (_context.Customers.Any())
                {
                    _context.Customers.RemoveRange(_context.Customers);
                }

                //ta bort artiklar från databasen
                if (_context.Articles.Any())
                {
                    _context.Articles.RemoveRange(_context.Articles);
                }

                var connector = new CustomerConnector
                {
                    FilterBy = Filter.Customer.Active,
                    Limit = 500
                };
                var customers = connector.Find();

                // Loopa sidindelning
                while (customers != null && int.Parse(customers.CurrentPage) <= int.Parse(customers.TotalPages))
                {
                    // För varje sida loopar vi alla kunder
                    foreach (var fortnoxCustomer in customers.CustomerSubset)
                    {
                        var customer = new Models.Customer
                        {
                            CustomerNumber = fortnoxCustomer.CustomerNumber,
                            Address1 = fortnoxCustomer.Address1,
                            Address2 = fortnoxCustomer.Address2,
                            City = fortnoxCustomer.City,
                            Name = fortnoxCustomer.Name,
                            OrganisationNumber = fortnoxCustomer.OrganisationNumber,
                            ZipCode = fortnoxCustomer.ZipCode
                        };

                        _context.Customers.Add(customer);
                    }

                    // Byt till nästa sida
                    connector.Page = int.Parse(customers.CurrentPage) + 1;
                    customers = connector.Find();
                }

                var articleConnector = new ArticleConnector
                {
                    FilterBy = Filter.Article.Active,
                    Limit = 500
                };
                var articles = articleConnector.Find();

                while (articles != null && int.Parse(articles.CurrentPage) <= int.Parse(articles.TotalPages))
                {
                    // För varje sida loopar vi alla artiklar
                    foreach (var fortnoxArticle in articles.ArticleSubset)
                    {
                        var article = new Models.Article
                        {
                            ArticleNumber = fortnoxArticle.ArticleNumber,
                            Name = fortnoxArticle.Description,
                            SalesPrice = float.Parse(fortnoxArticle.SalesPrice),
                            Unit = fortnoxArticle.Unit
                        };

                        _context.Articles.Add(article);
                    }

                    articleConnector.Page = int.Parse(articles.CurrentPage) + 1;
                    articles = articleConnector.Find();
                }

                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Models.ErrorResponse(ex.Message));
                throw;
            }

        }
    }
}