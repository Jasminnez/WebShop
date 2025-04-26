using System;
using AbySalto.Mid.Application.DTOs;

namespace AbySalto.Mid.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(int skip = 0, int limit = 10);
    Task<ProductDto?> GetByIdAsync(int id);
}
