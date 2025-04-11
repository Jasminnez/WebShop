using System;
using AbySalto.Mid.Application.DTOs;

namespace AbySalto.Mid.Application.Interfaces;

public interface IFavoriteService
{
    Task AddToFavoritesAsync(int userId, int productId);
    Task RemoveFromFavoritesAsync(int userId, int productId);
    Task<List<ProductDto>> GetFavoritesAsync(int userId);
}
