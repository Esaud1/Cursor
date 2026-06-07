using WComtismc.Models;

namespace WComtismc.Services;

public interface IProductCatalog
{
    IReadOnlyList<Product> GetAll();

    IReadOnlyList<string> GetCategories();

    Product? GetById(Guid id);

    IReadOnlyList<Product> Search(string? query, string? category, string? sort);
}
