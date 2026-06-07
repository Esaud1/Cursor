using WComtismc.Models;

namespace WComtismc.Services;

public interface ICartService
{
    IReadOnlyList<CartItem> GetItems();

    int GetItemCount();

    decimal GetSubtotal();

    void AddItem(Product product, int quantity = 1);

    void UpdateQuantity(Guid productId, int quantity);

    void RemoveItem(Guid productId);

    void Clear();
}
