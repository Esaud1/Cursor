namespace WComtismc.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string NameAr { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? OldPrice { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public int Stock { get; set; }

    public double Rating { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsNew { get; set; }

    public int DiscountPercent => OldPrice is > 0 and var old && old > Price
        ? (int)Math.Round((old - Price) / old * 100)
        : 0;
}
