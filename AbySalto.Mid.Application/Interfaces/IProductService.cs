using System;
using AbySalto.Mid.Application.DTOs;

namespace AbySalto.Mid.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
}
