using Database;
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
        private  DataContext _context;

        public SaleController(DataContext context)
        {
            _context = context;
        }

        // POST: api/Sales/5
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(SaleVM saleVM)
        {
            // string customerNumber = (saleVM != null && saleVM.SelectedCustomer != null && Convert.ToInt32(saleVM.SelectedCustomer.CustomerNumber) > 0) ? saleVM.SelectedCustomer.CustomerNumber : "";
            using var transaction = _context.Database.BeginTransaction();
            {
                try
                {
                    List<SaleArticle> saleArticles = new List<SaleArticle>();
                    //if (saleVM != null && saleVM.ArticleRows != null && Convert.ToInt32(saleVM.ArticleRows) > 0)
                    {
                        foreach (var item in saleVM.ArticleRows)
                        {
                            saleArticles.Add(new SaleArticle()
                            {
                                ArticleNumber = item.ArticleNumber,
                                Price = item.SalesPrice,
                                Quantity = item.Quantity,
                                TotalSum = item.SalesPrice * item.Quantity,
                                Description = item.Description,
                            });
                        }
                    }
                    Sale sale = new Sale();

                    sale.CustomerNumber = saleVM.SelectedCustomer.CustomerNumber;
                    sale.YourReference = saleVM.Reference;
                    sale.SaleArticles = saleArticles;
                    sale.DateCreated = saleVM.DateCreated;
                    sale.DateSold = saleVM.DateSold;
                    sale.StatusId = (Sale.Status)saleVM.StatusId;
                    // HACK: Fixxa
                   // sale.UserId = 2;

                    _context.Sales.Add(sale);

                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
            return CreatedAtAction(nameof(GetSale), new { id = saleVM.Id }, saleVM);
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
            return Ok(sale);
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
        public async Task<IActionResult> PutSale(int id, [FromBody] Sale sale)
        {
            var existingSale = await _context.Sales.FindAsync(id);
            if (existingSale is null)
            {
                return NotFound();
            }
            if (id != sale.Id)
            {
                return BadRequest();
            }
            _context.Entry(sale).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}