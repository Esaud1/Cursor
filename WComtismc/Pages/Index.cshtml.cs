using Microsoft.AspNetCore.Mvc.RazorPages;
using WComtismc.Models;
using WComtismc.Services;

namespace WComtismc.Pages;

public class IndexModel : PageModel
{
    private readonly IProductCatalog _catalog;

    public IndexModel(IProductCatalog catalog)
    {
        _catalog = catalog;
    }

    public IReadOnlyList<string> Categories { get; private set; } = [];

    public IReadOnlyList<Product> FeaturedProducts { get; private set; } = [];

    public IReadOnlyList<Product> NewProducts { get; private set; } = [];

    public void OnGet()
    {
        Categories = _catalog.GetCategories();
        FeaturedProducts = _catalog.GetAll().Where(product => product.IsFeatured).Take(4).ToList();
        NewProducts = _catalog.GetAll().Where(product => product.IsNew).Take(4).ToList();
    }

    public static string GetCategoryIcon(string category) => category switch
    {
        "مكياج" => "💄",
        "العناية بالبشرة" => "✨",
        "عطور" => "🌸",
        "العناية بالشعر" => "💇",
        "الأظافر" => "💅",
        _ => "🛍️"
    };
}
