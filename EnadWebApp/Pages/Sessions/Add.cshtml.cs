using System.ComponentModel.DataAnnotations;
using EnadWebApp.Models;
using EnadWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnadWebApp.Pages.Sessions;

public class AddModel : PageModel
{
    private readonly ISessionStore _sessionStore;
    private readonly IWebHostEnvironment _environment;

    public AddModel(ISessionStore sessionStore, IWebHostEnvironment environment)
    {
        _sessionStore = sessionStore;
        _environment = environment;
    }

    [BindProperty]
    public SessionInput Input { get; set; } = new();

    public IReadOnlyList<MemberCatalog.MemberOption> AvailableMembers { get; private set; } = [];

    public void OnGet()
    {
        AvailableMembers = MemberCatalog.GetAll();
        Input.SessionDate = DateTime.Now.AddDays(1).Date.AddHours(10);
        Input.AgendaItems.Add(new AgendaItemInput());
    }

    public async Task<IActionResult> OnPostAsync()
    {
        AvailableMembers = MemberCatalog.GetAll();

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

        var sessionId = Guid.NewGuid();
        var members = MemberCatalog.GetByIds(Input.SelectedMembers);
        var invitedPeople = ParseInvitedPeople(Input.InvitedPeople);
        var attachments = await SaveAttachmentsAsync(sessionId, Input.Attachments);

        var session = new SessionRecord
        {
            Id = sessionId,
            Title = Input.Title.Trim(),
            Description = Input.Description.Trim(),
            SessionDate = Input.SessionDate,
            CreatedAt = DateTime.Now,
            Members = members.Select(member => new SessionMember
            {
                Id = member.Id,
                Name = member.Name
            }).ToList(),
            InvitedPeople = invitedPeople,
            AgendaItems = Input.AgendaItems.Select(item => new SessionAgendaItem
            {
                Topic = item.Topic.Trim(),
                Duration = item.Duration?.Trim()
            }).ToList(),
            Attachments = attachments
        };

        _sessionStore.Add(session);

        return RedirectToPage("/Sessions/Details", new { id = sessionId });
    }

    private static List<string> ParseInvitedPeople(string? invitedPeople)
    {
        if (string.IsNullOrWhiteSpace(invitedPeople))
        {
            return [];
        }

        return invitedPeople
            .Split(['\r', '\n', ',', ';'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct()
            .ToList();
    }

    private async Task<List<SessionAttachment>> SaveAttachmentsAsync(Guid sessionId, List<IFormFile>? files)
    {
        if (files == null || files.Count == 0)
        {
            return [];
        }

        var uploadsRoot = Path.Combine(_environment.WebRootPath, "uploads", "sessions", sessionId.ToString());
        Directory.CreateDirectory(uploadsRoot);

        var attachments = new List<SessionAttachment>();

        foreach (var file in files.Where(file => file.Length > 0))
        {
            var safeFileName = Path.GetFileName(file.FileName);
            var storedFileName = $"{Guid.NewGuid():N}_{safeFileName}";
            var fullPath = Path.Combine(uploadsRoot, storedFileName);

            await using var stream = System.IO.File.Create(fullPath);
            await file.CopyToAsync(stream);

            attachments.Add(new SessionAttachment
            {
                FileName = safeFileName,
                RelativePath = Path.Combine("uploads", "sessions", sessionId.ToString(), storedFileName).Replace('\\', '/'),
                Size = file.Length
            });
        }

        return attachments;
    }

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
