using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WComtismc.Models;
using WComtismc.Services;

namespace WComtismc.Pages.Orders;

public class ConfirmationModel : PageModel
{
    private readonly IOrderStore _orderStore;

    public ConfirmationModel(IOrderStore orderStore)
    {
        _orderStore = orderStore;
    }

    public Order Order { get; private set; } = null!;

    public IActionResult OnGet(Guid id)
    {
        var order = _orderStore.GetById(id);
        if (order == null)
        {
            return RedirectToPage("/Index");
        }

        Order = order;
        return Page();
    }

}
