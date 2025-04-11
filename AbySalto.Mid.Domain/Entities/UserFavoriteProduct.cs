using System;

namespace AbySalto.Mid.Domain.Entities;

public class UserFavoriteProduct
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
