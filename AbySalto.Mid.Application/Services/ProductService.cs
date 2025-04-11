using System;
using System.Net.Http.Json;
using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Infrastructure.Persistence;

namespace AbySalto.Mid.Application.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;
    public ProductService(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }
    public async Task<List<ProductDto>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<DummyProductListDto>("https://dummyjson.com/products");
        var products = response?.Products ?? new();

        return products;
    }
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto?>($"https://dummyjson.com/products/{id}");
    }
}
