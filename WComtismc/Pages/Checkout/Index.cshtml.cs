using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WComtismc.Models;
using WComtismc.Services;

namespace WComtismc.Pages.Checkout;

public class IndexModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly IOrderStore _orderStore;

    public IndexModel(ICartService cartService, IOrderStore orderStore)
    {
        _cartService = cartService;
        _orderStore = orderStore;
    }

    [BindProperty]
    public CheckoutInput Input { get; set; } = new();

    public decimal Subtotal { get; private set; }

    public decimal Shipping { get; private set; }

    public decimal Total { get; private set; }

    public IActionResult OnGet()
    {
        if (_cartService.GetItems().Count == 0)
        {
            return RedirectToPage("/Cart/Index");
        }

        CalculateTotals();
        return Page();
    }

    public IActionResult OnPost()
    {
        var cartItems = _cartService.GetItems();
        if (cartItems.Count == 0)
        {
            return RedirectToPage("/Cart/Index");
        }

        CalculateTotals();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = Input.FullName.Trim(),
            Email = Input.Email.Trim(),
            Phone = Input.Phone.Trim(),
            Address = Input.Address.Trim(),
            City = Input.City.Trim(),
            PaymentMethod = Input.PaymentMethod,
            Subtotal = Subtotal,
            Shipping = Shipping,
            Total = Total,
            CreatedAt = DateTime.Now,
            Status = OrderStatus.Confirmed,
            UserId = GetUserId(),
            Items = cartItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                Total = item.Total
            }).ToList()
        };

        _orderStore.Create(order);
        _cartService.Clear();

        return RedirectToPage("/Orders/Confirmation", new { id = order.Id });
    }

    private Guid? GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(id, out var userId) ? userId : null;
    }

    private void CalculateTotals()
    {
        Subtotal = _cartService.GetSubtotal();
        Shipping = Subtotal >= 300 ? 0 : 25;
        Total = Subtotal + Shipping;
    }

    public class CheckoutInput
    {
        [Required(ErrorMessage = "الاسم مطلوب.")]
        [Display(Name = "الاسم الكامل")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "بريد إلكتروني غير صالح.")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الجوال مطلوب.")]
        [Phone(ErrorMessage = "رقم جوال غير صالح.")]
        [Display(Name = "رقم الجوال")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "العنوان مطلوب.")]
        [Display(Name = "العنوان")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "المدينة مطلوبة.")]
        [Display(Name = "المدينة")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "طريقة الدفع مطلوبة.")]
        [Display(Name = "طريقة الدفع")]
        public string PaymentMethod { get; set; } = "cod";
    }
}
