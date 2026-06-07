using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnadWebApp.Pages.Search;

public class IndexModel : PageModel
{
    public string? Query { get; set; }

    public void OnGet(string? query)
    {
        Query = query;
    }
}
