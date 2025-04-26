using System.Security.Claims;
using AbySalto.Mid.Application.Interfaces;
using API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers
{
    [Authorize]
    public class CartController : BaseApiController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = GetUserIdFromClaims();
            await _cartService.AddToCartAsync(userId, productId, quantity);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserIdFromClaims();
            var cart = await _cartService.GetCartProductsAsync(userId);
            return Ok(cart);
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = GetUserIdFromClaims();
            await _cartService.RemoveFromCartAsync(userId, productId);
            return Ok();
        }
        private int GetUserIdFromClaims()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }
    }
}
