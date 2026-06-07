using EnadWebApp.Models;
using EnadWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnadWebApp.Pages.Sessions;

public class DetailsModel : PageModel
{
    private readonly ISessionStore _sessionStore;

    public DetailsModel(ISessionStore sessionStore)
    {
        _sessionStore = sessionStore;
    }

    public SessionRecord Session { get; private set; } = null!;

    public IActionResult OnGet(Guid id)
    {
        var session = _sessionStore.GetById(id);
        if (session == null)
        {
            return RedirectToPage("/Sessions/Active");
        }

        Session = session;
        return Page();
    }

    public static string FormatFileSize(long bytes)
    {
        if (bytes < 1024)
        {
            return $"{bytes} بايت";
        }

        if (bytes < 1024 * 1024)
        {
            return $"{bytes / 1024.0:0.#} ك.ب";
        }

        return $"{bytes / (1024.0 * 1024.0):0.#} م.ب";
    }
}
