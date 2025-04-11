using System;

namespace AbySalto.Mid.Application.DTOs;

public class CartDto
{
    public int ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
}
