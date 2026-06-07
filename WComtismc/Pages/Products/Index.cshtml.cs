using Microsoft.AspNetCore.Mvc.RazorPages;
using WComtismc.Models;
using WComtismc.Services;

namespace WComtismc.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductCatalog _catalog;

    public IndexModel(IProductCatalog catalog)
    {
        _catalog = catalog;
    }

    public IReadOnlyList<Product> Products { get; private set; } = [];

    public IReadOnlyList<string> Categories { get; private set; } = [];

    public string? Query { get; private set; }

    public string? Category { get; private set; }

    public string Sort { get; private set; } = "featured";

    public void OnGet(string? q, string? category, string? sort)
    {
        Query = q;
        Category = category;
        Sort = string.IsNullOrWhiteSpace(sort) ? "featured" : sort;
        Categories = _catalog.GetCategories();
        Products = _catalog.Search(q, category, Sort);
    }
}
