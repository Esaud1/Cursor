using WComtismc.Models;

namespace WComtismc.Services;

public interface IOrderStore
{
    Order Create(Order order);

    Order? GetById(Guid id);

    IReadOnlyList<Order> GetByUserId(Guid userId);

    IReadOnlyList<Order> GetAll();
}
