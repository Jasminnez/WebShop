using System.Security.Claims;
using AbySalto.Mid.Application.Interfaces;
using API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.WebApi.Controllers
{
    [Authorize]
    public class FavoriteController : BaseApiController
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToFavorites(int productId)
        {
            var userId = GetUserIdFromClaims();
            await _favoriteService.AddToFavoritesAsync(userId, productId);
            return Ok();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromFavorites(int productId)
        {
            var userId = GetUserIdFromClaims();
            await _favoriteService.RemoveFromFavoritesAsync(userId, productId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = GetUserIdFromClaims();
            var favorites = await _favoriteService.GetFavoriteProductsAsync(userId);
            return Ok(favorites);
        }

        private int GetUserIdFromClaims()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }
    }
}
