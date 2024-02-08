using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Data;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]

    //authentication 
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly inherDbContext _dbContext;

        public ProductController(inherDbContext DbContext)
        {
            _dbContext = DbContext;
        }


        // CRUD Operations :

        // Create
        [HttpPost]
        [Route("")]

        public ActionResult<int>CreateProduct(Product product)
        {
            product.Id = 0;
            _dbContext.Set<Product>().Add(product);
            _dbContext.SaveChanges();
            return Ok(product.Id);
        }


        // READ
        [HttpGet]
        [Route("")]
        
        [AllowAnonymous]
        public ActionResult<IEnumerable<Product>> GetProducts() 
        {
            var records = _dbContext.Set<Product>().ToList();
            if (records != null )
            {
                var userName = User.Identity.Name;
                var UserId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok(records); 
            }
            else
            {
                Console.WriteLine("NOT FOUND !");
                return BadRequest();
            }
        
        }

        // Get product by Id :
        [HttpGet]
        [Route("{id}")] // Key == int id (parameter) [FromRoute(Name = "Key")]
        
        public ActionResult<Product> GetProductById(int id)
        {
            var inID = _dbContext.Set<Product>().Find(id);
            if (inID != null )
            {
                _dbContext.SaveChanges();
                return Ok(inID);
            }
            else
            {
                Console.WriteLine("NOT FOUND ! ");
                return NotFound();
            }
        }


        // UPDATE 
        [HttpPut]
        [Route("")]
        public ActionResult UpdateProduct(Product product)
        {
            // find Id to update the product: ++ if condition if exisit
            var existingProduct = _dbContext.Set<Product>().Find(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.SKU = product.SKU;
                _dbContext.Set<Product>().Update(existingProduct);
                _dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                Console.WriteLine("NOT FOUND!");
                return NotFound();
            }
            
        }


        // DELETE
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var exiProduct = _dbContext.Set<Product>().Find(id);
            if (exiProduct != null)
            {
                _dbContext.Set<Product>().Remove(exiProduct);
                _dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                Console.WriteLine("NOT FOUND!");
                return NotFound();
            }
        }

    }
}
