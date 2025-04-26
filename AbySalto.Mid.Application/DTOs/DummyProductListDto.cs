using System;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.DTOs;

public class DummyProductListDto
{
    public required List<ProductDto> Products { get; set; }
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}
