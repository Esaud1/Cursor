using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnadWebApp.Pages.Sessions;

public class AddModel : PageModel
{
    [BindProperty]
    public SessionInput Input { get; set; } = new();

    public IReadOnlyList<MemberOption> AvailableMembers { get; private set; } = [];

    public bool ShowSuccess { get; private set; }

    public void OnGet()
    {
        AvailableMembers = GetMembers();
        Input.SessionDate = DateTime.Now.AddDays(1).Date.AddHours(10);
        Input.AgendaItems.Add(new AgendaItemInput());
    }

    public IActionResult OnPost()
    {
        AvailableMembers = GetMembers();

        Input.AgendaItems = Input.AgendaItems
            .Where(item => !string.IsNullOrWhiteSpace(item.Topic))
            .ToList();

        if (Input.AgendaItems.Count == 0)
        {
            ModelState.AddModelError("Input.AgendaItems", "يجب إضافة بند واحد على الأقل في الأجندة.");
            Input.AgendaItems.Add(new AgendaItemInput());
        }

        if (Input.SelectedMembers.Count == 0)
        {
            ModelState.AddModelError("Input.SelectedMembers", "يجب اختيار عضو واحد على الأقل.");
        }

        if (Input.Attachments?.Count > 0)
        {
            const long maxFileSize = 10 * 1024 * 1024;
            foreach (var file in Input.Attachments)
            {
                if (file.Length > maxFileSize)
                {
                    ModelState.AddModelError("Input.Attachments", $"الملف '{file.FileName}' يتجاوز الحد الأقصى 10 ميجابايت.");
                }
            }
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        ShowSuccess = true;
        return Page();
    }

    private static IReadOnlyList<MemberOption> GetMembers() =>
    [
        new("1", "أحمد محمد العلي"),
        new("2", "سارة خالد الحربي"),
        new("3", "فهد عبدالله القحطاني"),
        new("4", "نورة سعد الدوسري"),
        new("5", "محمد علي الشمري"),
        new("6", "ريم يوسف الزهراني")
    ];

    public record MemberOption(string Id, string Name);

    public class SessionInput
    {
        [Required(ErrorMessage = "عنوان الجلسة مطلوب.")]
        [StringLength(200, ErrorMessage = "العنوان يجب ألا يتجاوز 200 حرف.")]
        [Display(Name = "عنوان الجلسة")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "الوصف المختصر مطلوب.")]
        [StringLength(500, ErrorMessage = "الوصف يجب ألا يتجاوز 500 حرف.")]
        [Display(Name = "وصف مختصر")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "قائمة الاعضاء")]
        public List<string> SelectedMembers { get; set; } = [];

        [Required(ErrorMessage = "تاريخ الجلسة مطلوب.")]
        [Display(Name = "تاريخ الجلسة")]
        public DateTime SessionDate { get; set; }

        [Display(Name = "الاشخاص المدعوين")]
        public string? InvitedPeople { get; set; }

        [Display(Name = "مرفقات الجلسة")]
        public List<IFormFile>? Attachments { get; set; }

        public List<AgendaItemInput> AgendaItems { get; set; } = [];
    }

    public class AgendaItemInput
    {
        [Required(ErrorMessage = "موضوع البند مطلوب.")]
        [Display(Name = "الموضوع")]
        public string Topic { get; set; } = string.Empty;

        [Display(Name = "المدة")]
        public string? Duration { get; set; }
    }
}
