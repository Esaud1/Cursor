using EnadWebApp.Models;
using EnadWebApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnadWebApp.Pages.Sessions;

public class ActiveModel : PageModel
{
    private readonly ISessionStore _sessionStore;

    public ActiveModel(ISessionStore sessionStore)
    {
        _sessionStore = sessionStore;
    }

    public IReadOnlyList<SessionRecord> Sessions { get; private set; } = [];

    public void OnGet()
    {
        Sessions = _sessionStore.GetAll();
    }
}
