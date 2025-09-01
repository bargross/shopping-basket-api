using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using shopping_basket_api.Models;
using shopping_basket_api.Services;

namespace shopping_basket_api.Controllers
{
    [ApiController]
    [Route("basket")]
    public class ShoppingController(IShoppingService shoppingService) : ControllerBase
    {
        [HttpPost("id/{basketId}/add")]
        public async Task<IActionResult> AddBasketITems([FromRoute] string basketId, [FromBody] AddBasketItemRequest request)
        {
            try
            {
                await shoppingService.AddToBasketAsync(basketId, request);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("id/{basketId}/add-discount")]
        public async Task<IActionResult> AddDiscountToBasket([FromRoute] string basketId, [FromBody] Discount request)
        {
            try
            {
                await shoppingService.AddDiscountCodeAsync(basketId, request);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("id/{basketId}/item/{itemId}/remove")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute] string basketId, [FromRoute] string itemId)
        {
            try
            {
                await shoppingService.RemoveItemAsync(basketId, itemId);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("id/{basketId}/total")]
        public async Task<IActionResult> GetBasketTotal([FromRoute] string basketId, [FromQuery] bool withVAT)
        {
            try
            {
                await shoppingService.GetBasketTotalAsync(basketId, withVAT);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("id/{basketId}/checkout")]
        public async Task<IActionResult> Checkout([FromRoute] string basketId)
        {
            try
            {
                await shoppingService.CheckoutAsync(basketId);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
