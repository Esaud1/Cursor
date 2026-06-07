using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WComtismc.Models;
using WComtismc.Services;

namespace WComtismc.Pages.Products;

public class DetailsModel : PageModel
{
    private readonly IProductCatalog _catalog;
    private readonly ICartService _cartService;

    public DetailsModel(IProductCatalog catalog, ICartService cartService)
    {
        _catalog = catalog;
        _cartService = cartService;
    }

    public Product Product { get; private set; } = null!;

    public IReadOnlyList<Product> RelatedProducts { get; private set; } = [];

    public IActionResult OnGet(Guid id)
    {
        var product = _catalog.GetById(id);
        if (product == null)
        {
            return RedirectToPage("/Products/Index");
        }

        Product = product;
        RelatedProducts = _catalog.GetAll()
            .Where(p => p.Category == product.Category && p.Id != product.Id)
            .Take(4)
            .ToList();

        return Page();
    }

    public IActionResult OnPostAddToCart(Guid id, int quantity = 1)
    {
        var product = _catalog.GetById(id);
        if (product == null)
        {
            return RedirectToPage("/Products/Index");
        }

        _cartService.AddItem(product, Math.Max(1, quantity));
        return RedirectToPage("/Cart/Index");
    }
}
