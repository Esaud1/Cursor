namespace WComtismc.Models;

public class Order
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;

    public List<OrderItem> Items { get; set; } = [];

    public decimal Subtotal { get; set; }

    public decimal Shipping { get; set; }

    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; }

    public OrderStatus Status { get; set; }

    public Guid? UserId { get; set; }
}

public class OrderItem
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal Total { get; set; }
}

public enum OrderStatus
{
    Pending,
    Confirmed,
    Shipped,
    Delivered
}
