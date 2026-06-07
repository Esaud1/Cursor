using System.Collections.Concurrent;
using WComtismc.Models;

namespace WComtismc.Services;

public class OrderStore : IOrderStore
{
    private readonly ConcurrentDictionary<Guid, Order> _orders = new();

    public Order Create(Order order)
    {
        _orders[order.Id] = order;
        return order;
    }

    public Order? GetById(Guid id) =>
        _orders.TryGetValue(id, out var order) ? order : null;

    public IReadOnlyList<Order> GetByUserId(Guid userId) =>
        _orders.Values
            .Where(order => order.UserId == userId)
            .OrderByDescending(order => order.CreatedAt)
            .ToList();

    public IReadOnlyList<Order> GetAll() =>
        _orders.Values.OrderByDescending(order => order.CreatedAt).ToList();
}
