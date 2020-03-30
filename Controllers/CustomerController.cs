using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E37SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        //[EnableCors("PolicyName")]
        [HttpGet]
        public IEnumerable<Models.Customer> GetCustomers()
        {
            return _context.Customers;
        }

        // GET: api/Customers/5
        //[EnableCors]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] string id)
        {

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet("find/{query}")]
        public  IEnumerable<Models.Customer> FindCustomer([FromRoute] string query)
        {
            return _context.Customers.Where(c => c.CustomerNumber.Contains(query) || c.Name.Contains(query));
        }
    }
}
