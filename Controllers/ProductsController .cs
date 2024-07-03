using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.DTOs;
using Shop.Service.IService;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public ActionResult<ProductDto> AddProduct(ProductDto productDto)
        {
             _productService.CreateProduct(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.ProductId }, productDto);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }
            _productService.UpdateProduct(productDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }

    }
}