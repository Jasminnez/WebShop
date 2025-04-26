using System;
using AbySalto.Mid.Application.DTOs;

namespace AbySalto.Mid.Application.Interfaces;

public interface ICartService
{
    Task AddToCartAsync(int userId, int productId, int quantity);
    Task RemoveFromCartAsync(int userId, int productId);
    Task<List<CartItemDto>> GetCartAsync(int userId);
    Task<List<ProductDto>> GetCartProductsAsync(int userId);
}
