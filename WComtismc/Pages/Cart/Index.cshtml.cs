using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WComtismc.Services;

namespace WComtismc.Pages.Cart;

public class IndexModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly IProductCatalog _catalog;

    public IndexModel(ICartService cartService, IProductCatalog catalog)
    {
        _cartService = cartService;
        _catalog = catalog;
    }

    public decimal Subtotal { get; private set; }

    public decimal Shipping { get; private set; }

    public decimal Total { get; private set; }

    public void OnGet()
    {
        CalculateTotals();
    }

    public IActionResult OnPostAdd(Guid productId)
    {
        var product = _catalog.GetById(productId);
        if (product != null)
        {
            _cartService.AddItem(product);
        }

        return RedirectToPage("/Cart/Index");
    }

    public IActionResult OnPostUpdate(Guid productId, int quantity)
    {
        _cartService.UpdateQuantity(productId, quantity);
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(Guid productId)
    {
        _cartService.RemoveItem(productId);
        return RedirectToPage();
    }

    public IActionResult OnPostClear()
    {
        _cartService.Clear();
        return RedirectToPage();
    }

    private void CalculateTotals()
    {
        Subtotal = _cartService.GetSubtotal();
        Shipping = Subtotal >= 300 || Subtotal == 0 ? 0 : 25;
        Total = Subtotal + Shipping;
    }
}
