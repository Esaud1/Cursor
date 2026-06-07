using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnadWebApp.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IConfiguration _configuration;

    public LoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "يرجى إدخال بريد إلكتروني صالح.")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "تذكرني")]
        public bool RememberMe { get; set; }
    }

    public IActionResult OnGet(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToPage("/Index");
        }

        ReturnUrl = returnUrl;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var validEmail = _configuration["Login:Email"] ?? "admin@enad.com";
        var validPassword = _configuration["Login:Password"] ?? "Password123!";

        if (!string.Equals(Input.Email, validEmail, StringComparison.OrdinalIgnoreCase) ||
            Input.Password != validPassword)
        {
            ModelState.AddModelError(string.Empty, "البريد الإلكتروني أو كلمة المرور غير صحيحة.");
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, Input.Email),
            new(ClaimTypes.Email, Input.Email)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = Input.RememberMe,
            ExpiresUtc = Input.RememberMe
                ? DateTimeOffset.UtcNow.AddDays(14)
                : DateTimeOffset.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProperties);

        return LocalRedirect(returnUrl ?? Url.Page("/Index")!);
    }
}
