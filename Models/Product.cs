using System;
using System.Collections.Generic;
using GourmetShopWebApp.Models;

namespace GourmetShopWebApp.Models;

public partial class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public int SupplierId { get; set; }
    public decimal? UnitPrice { get; set; }
    public string? Package { get; set; }
    public bool IsDiscontinued { get; set; }
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual Supplier Supplier { get; set; } = null!;

    // Sale properties
    public bool IsOnSale { get; set; }
    public decimal? SalePrice { get; set; }
    public DateTime? SaleStart { get; set; }
    public DateTime? SaleEnd { get; set; }

    // Computed property that returns the sale price if the product is on sale during the valid period; otherwise, it returns the regular unit price.
    public decimal? CurrentPrice
    {
        get
        {
            if (IsOnSale && SalePrice.HasValue && SaleStart.HasValue && SaleEnd.HasValue)
            {
                DateTime now = DateTime.Now;
                if (now >= SaleStart.Value && now <= SaleEnd.Value)
                {
                    return SalePrice.Value;
                }
            }
            return UnitPrice;
        }
    }
}