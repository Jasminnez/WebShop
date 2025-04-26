using System;
using System.Security.Claims;
using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Application.Services;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        var cart = await _context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId); 

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
        }

        var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<CartItemDto>> GetCartAsync(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            return new List<CartItemDto>();
        }

        return cart.Items.Select(i => new CartItemDto
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
        }).ToList();    
    }

    public async Task<List<ProductDto>> GetCartProductsAsync(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            return new List<ProductDto>();
        }

        var productDtos = new List<ProductDto>();
        IProductService ProductService = new ProductService(new HttpClient(), _context);
        foreach (var item in cart.Items)
        {
            var product = await ProductService.GetByIdAsync(item.ProductId);

            if (product != null)
            {
                productDtos.Add(product);
            }
        }

        return productDtos;
    }

    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart != null)
        {
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            
            if (item != null)
            {
                cart.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }

}
