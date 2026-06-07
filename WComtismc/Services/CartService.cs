using System.Text.Json;
using WComtismc.Models;

namespace WComtismc.Services;

public class CartService : ICartService
{
    private const string CartSessionKey = "WComtismc_Cart";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IReadOnlyList<CartItem> GetItems() => GetCart();

    public int GetItemCount() => GetCart().Sum(item => item.Quantity);

    public decimal GetSubtotal() => GetCart().Sum(item => item.Total);

    public void AddItem(Product product, int quantity = 1)
    {
        var cart = GetCart();
        var existing = cart.FirstOrDefault(item => item.ProductId == product.Id);

        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.NameAr,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = quantity
            });
        }

        SaveCart(cart);
    }

    public void UpdateQuantity(Guid productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(cartItem => cartItem.ProductId == productId);

        if (item == null)
        {
            return;
        }

        if (quantity <= 0)
        {
            cart.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }

        SaveCart(cart);
    }

    public void RemoveItem(Guid productId)
    {
        var cart = GetCart();
        cart.RemoveAll(item => item.ProductId == productId);
        SaveCart(cart);
    }

    public void Clear() => SaveCart([]);

    private List<CartItem> GetCart()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null)
        {
            return [];
        }

        var json = session.GetString(CartSessionKey);
        return string.IsNullOrEmpty(json)
            ? []
            : JsonSerializer.Deserialize<List<CartItem>>(json) ?? [];
    }

    private void SaveCart(List<CartItem> cart)
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null)
        {
            return;
        }

        session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
    }
}
