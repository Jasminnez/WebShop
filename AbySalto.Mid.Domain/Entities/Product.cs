using System;

namespace AbySalto.Mid.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public required decimal Price { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
