using System;
using Newtonsoft.Json;
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
    public async Task<List<ProductDto>> GetAllAsync(int skip = 0, int limit = 10)
    {
        // var response = await _httpClient.GetFromJsonAsync<DummyProductListDto>("https://dummyjson.com/products");
        // var products = response?.Products ?? new();
        var response = await _httpClient.GetAsync($"https://dummyjson.com/products?skip={skip}&limit={limit}");

        if (!response.IsSuccessStatusCode)
        {
            return new List<ProductDto>();
        }

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<DummyProductListDto>(content);

        if (result == null)
        {
            return new List<ProductDto>();
        }
        if (!response.IsSuccessStatusCode)
        {
            return new List<ProductDto>();
        }
        var productDtos = result.Products.Select(p => new ProductDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            DiscountPercentage = p.DiscountPercentage,
            Rating = p.Rating,
            Stock = p.Stock,
            Brand = p.Brand,
            Sku = p.Sku,
            Weight = p.Weight,
            WarrantyInformation = p.WarrantyInformation,
            ShippingInformation = p.ShippingInformation,
            AvailabilityStatus = p.AvailabilityStatus,
            ReturnPolicy = p.ReturnPolicy,
            MinimumOrderQuantity = p.MinimumOrderQuantity,
            Thumbnail = p.Thumbnail
        }).ToList();


        return productDtos;
    }
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto?>($"https://dummyjson.com/products/{id}");
    }
}
