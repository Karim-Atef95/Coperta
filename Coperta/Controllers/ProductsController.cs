using Coperta.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coperta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //k generated with quick actions 
        private static StoreContext _context;
        //k inject the db context to fetch data from db to client 
        //k when we hit the controller with http it'll create an instance
        //and invoke the constructor which will create an instance of dbcontext
        //the lifetime of the storecontext is during the http request call only
        public ProductsController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        //k this is sync and will wait products to finish query,
        //so we make it async and add task that will pass the request to delegate
        //and won't block the thread
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //k list will execute a select query on db
            //
            var products = await _context.Products.ToListAsync();
            return Ok(products);
            //return "this will be a list of products";
        }
        /*me
         * it gave me an error when both were just [HttpGet]
         * because api didn't know which Get method to call
         * so differentiated it with {id}
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return Ok(product);
        }

    }
}
