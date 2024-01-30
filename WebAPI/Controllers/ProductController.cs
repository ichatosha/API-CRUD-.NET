using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
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
        public ActionResult<IEnumerable<Product>> GetProducts() 
        {
            var records = _dbContext.Set<Product>().ToList();
            if (records != null )
            {

                return Ok(records); 
            }
            else
            {
                Console.WriteLine("NOT FOUND !");
                return BadRequest();
            }
        
        }
        [HttpGet]
        [Route("{id}")]
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
