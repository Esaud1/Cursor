using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WComtismc.Models;
using WComtismc.Services;

namespace WComtismc.Pages.Orders;

[Authorize]
public class MyOrdersModel : PageModel
{
    private readonly IOrderStore _orderStore;

    public MyOrdersModel(IOrderStore orderStore)
    {
        _orderStore = orderStore;
    }

    public IReadOnlyList<Order> Orders { get; private set; } = [];

    public IActionResult OnGet()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userId, out var id))
        {
            return RedirectToPage("/Account/Login");
        }

        Orders = _orderStore.GetByUserId(id);
        return Page();
    }
}
