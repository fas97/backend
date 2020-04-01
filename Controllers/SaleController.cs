using Database;
using E37SalesApi.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace E37SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private DataContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public SaleController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // POST: api/Sales/5
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<Sale>> PostSale([FromBody] SaleVMStr saleVMStr)
        {
            // string customerNumber = (saleVM != null && saleVM.SelectedCustomer != null && Convert.ToInt32(saleVM.SelectedCustomer.CustomerNumber) > 0) ? saleVM.SelectedCustomer.CustomerNumber : "";
            SaleVM saleVM = saleVMStr.SaleVm.GetObjectFronJson<SaleVM>();

            //using var transaction = _context.Database.BeginTransaction();
            {
                try
                {
                    //List<SaleArticle> saleArticles = new List<SaleArticle>();
                    //if (saleVM != null && saleVM.articleRows != null && saleVM.articleRows.Count > 0)
                    //{
                    //    foreach (var item in saleVM.articleRows)
                    //    {
                    //        saleArticles.Add(new SaleArticle()
                    //        {
                    //            ArticleNumber = item.article.articleNumber,
                    //            Price = item.article.salesPrice,
                    //            Quantity = item.quantity,
                    //            TotalSum = item.article.salesPrice * item.quantity,
                    //            Description = item.article.description,
                    //        });
                    //    }
                    //}
                    Sale sale = new Sale();

                    sale.CustomerNumber = saleVM.customer.customerNumber;
                    sale.YourReference = saleVM.reference;
                    //sale.SaleArticles = saleArticles;
                    sale.DateCreated = DateTime.Parse(saleVM.dateCreated.Substring(0, 10));
                    sale.DateSold = DateTime.Parse(saleVM.dateSold.Substring(0, 10));
                    sale.StatusId = (Status)saleVM.statusId;
                    sale.UserId = 1;
                    // HACK: Fixxa
                    // sale.UserId = 2;

                    _context.Sales.Add(sale);

                    await _context.SaveChangesAsync();
                    //transaction.Commit();
                }
                catch (Exception e)
                {
                    //transaction.Rollback();
                }
            }
            return CreatedAtAction(nameof(GetSale), new { id = saleVM.id }, saleVM);
        }

        // GET: api/Sales
        [HttpGet]
        public IEnumerable<Models.Sale> GetSales()
        {
            return _context.Sales;
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale([FromRoute] int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            SaleVM saleVM = new SaleVM();
            List<ArticleRow> articleRows = new List<ArticleRow>();
            Article art = await _context.Articles.FirstAsync();
            ArticleRow articleRow = new ArticleRow();

            articleRow.article = new ArticleVM();
            articleRow.article.articleNumber = art.ArticleNumber;
            articleRow.article.name = art.Name;
            articleRow.article.salesPrice = (int)art.SalesPrice;
            articleRow.article.unit = art.Unit;
            articleRows.Add(articleRow);
            saleVM.reference = sale.YourReference;
            saleVM.customer = new SelectedCustomer();
            saleVM.customer.customerNumber = sale.CustomerNumber;
            saleVM.dateCreated = sale.DateCreated.ToString();
            saleVM.dateCreated = sale.DateSold.ToString();
            saleVM.statusId = (int)sale.StatusId;
            saleVM.id = sale.Id;
            saleVM.articleRows = articleRows;

            return Ok(saleVM);
        }

        // DELETE: api/Sale1/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            Sale sale = _context.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            _context.SaveChanges();

            return Ok();
        }

        //PUT: api/Sales/5
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutSale(int id, [FromBody] SaleVMStr saleVMStr)
        {
            SaleVM saleVM = saleVMStr.SaleVm.GetObjectFronJson<SaleVM>();
            var existingSale = await _context.Sales.FindAsync(id);

            if (existingSale is null)
            {
                return NotFound();
            }
            if (id != saleVM.id)
            {
                return BadRequest();
            }

            //List<SaleArticle> saleArticles = new List<SaleArticle>();

            //foreach (ArticleRow item in saleVM.articleRows)
            //{
            //    saleArticles.Add(new SaleArticle()
            //    {
            //        ArticleNumber = item.article.articleNumber,
            //        Price = item.article.salesPrice,
            //        Quantity = item.quantity,
            //        TotalSum = item.article.salesPrice * item.quantity,
            //        Description = item.article.description
            //    });
            //}

            existingSale.CustomerNumber = saleVM.customer.customerNumber;
            existingSale.YourReference = saleVM.reference;
            existingSale.DateCreated = DateTime.Parse(saleVM.dateCreated.Substring(0, 10));
            existingSale.DateSold = DateTime.Parse(saleVM.dateSold.Substring(0, 10));
            existingSale.StatusId = (Status)saleVM.statusId;
            //existingSale.SaleArticles = saleArticles;

            try
            {
                _context.Entry(existingSale).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw (ex);
            }
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}