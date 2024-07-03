using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.DTOs;
using Shop.Service.IService;

namespace Shop.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class CartsController : ControllerBase 
        {
            private readonly ICartService _cartService;

            public CartsController(ICartService cartService)
            {
                _cartService = cartService;
            }

            [HttpGet]
            public ActionResult<IEnumerable<CartDto>> GetAll()
            {
                var carts = _cartService.GetAllCarts();
                return Ok(carts);
            }

            [HttpGet("{id}")]
            public ActionResult<CartDto> GetById(int id)
            {
                var cart = _cartService.GetCartById(id);
                if (cart == null)
                {
                    return NotFound();
                }   
                return Ok(cart);
            }

            [HttpPost]
            public ActionResult CreateCart([FromBody] CartDto cartDto)
            {
                _cartService.CreateCart(cartDto);
                return CreatedAtAction(nameof(GetById), new { id = cartDto.CartId }, cartDto);
            }

            [HttpPut("{id}")]
            public ActionResult UpdateCart(int id, [FromBody] CartDto cartDto)
            {
                if (id != cartDto.CartId)
                {
                    return BadRequest();
                }
                _cartService.UpdateCart(cartDto);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public ActionResult DeleteCart(int id)
            {
                _cartService.DeleteCart(id);
                return NoContent();
            }
        }
    
}
