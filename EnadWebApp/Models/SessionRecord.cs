namespace EnadWebApp.Models;

public class SessionRecord
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime SessionDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<SessionMember> Members { get; set; } = [];

    public List<string> InvitedPeople { get; set; } = [];

    public List<SessionAgendaItem> AgendaItems { get; set; } = [];

    public List<SessionAttachment> Attachments { get; set; } = [];

    public SessionStatus Status => SessionDate.Date >= DateTime.Today ? SessionStatus.Active : SessionStatus.Completed;
}

public class SessionMember
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}

public class SessionAgendaItem
{
    public string Topic { get; set; } = string.Empty;

    public string? Duration { get; set; }
}

public class SessionAttachment
{
    public string FileName { get; set; } = string.Empty;

    public string RelativePath { get; set; } = string.Empty;

    public long Size { get; set; }
}

public enum SessionStatus
{
    Active,
    Completed
}
