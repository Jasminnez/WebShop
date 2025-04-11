using System;

namespace AbySalto.Mid.Application.DTOs;

public class CartItemDto
{
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int Quantity { get; set; }
}
