using System;
using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Application.Services;

public class FavoriteService : IFavoriteService
{
    private readonly AppDbContext _context;

    public FavoriteService(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddToFavoritesAsync(int userId, int productId)
    {
        var exists = await _context.UserFavoriteProducts
            .AnyAsync(x => x.UserId == userId && x.ProductId == productId);

        if (!exists)
        {
            var favorite = new UserFavoriteProduct
            {
                UserId = userId,
                ProductId = productId
            };

            await _context.UserFavoriteProducts.AddAsync(favorite);
            await _context.SaveChangesAsync(); 
        }
    }

    public async Task<List<ProductDto>> GetFavoritesAsync(int userId)
    {
       var favorites = await _context.UserFavoriteProducts
        .Where(x => x.UserId == userId)
        .Include(x => x.Product)
        .Select(x => new ProductDto
        {
            Id = x.Product.Id,
            Title = x.Product.Title,
            Price = x.Product.Price,
            Description = x.Product.Description
        })
        .ToListAsync();

        return favorites;
    }

    public async Task RemoveFromFavoritesAsync(int userId, int productId)
    {
        var fav = await _context.UserFavoriteProducts
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

        if (fav != null)
        {
            _context.UserFavoriteProducts.Remove(fav);
            await _context.SaveChangesAsync();
        }
    }
}
