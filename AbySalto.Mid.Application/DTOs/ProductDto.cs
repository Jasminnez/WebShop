using System;

namespace AbySalto.Mid.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Rating { get; set; }
    public int Stock { get; set; }
    public string? Brand { get; set; }
    public string Sku { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public string WarrantyInformation { get; set; } = string.Empty;
    public string ShippingInformation { get; set; } = string.Empty;
    public string AvailabilityStatus { get; set; } = string.Empty;
    public string ReturnPolicy { get; set; } = string.Empty;
    public int MinimumOrderQuantity { get; set; }
    public string Thumbnail { get; set; } = string.Empty;
}
