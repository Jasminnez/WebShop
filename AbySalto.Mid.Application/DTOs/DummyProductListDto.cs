using System;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.DTOs;

public class DummyProductListDto
{
    public required List<ProductDto> Products { get; set; }
}
