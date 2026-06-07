namespace EnadWebApp.Services;

public static class MemberCatalog
{
    private static readonly IReadOnlyList<MemberOption> Members =
    [
        new("1", "أحمد محمد العلي"),
        new("2", "سارة خالد الحربي"),
        new("3", "فهد عبدالله القحطاني"),
        new("4", "نورة سعد الدوسري"),
        new("5", "محمد علي الشمري"),
        new("6", "ريم يوسف الزهراني")
    ];

    public static IReadOnlyList<MemberOption> GetAll() => Members;

    public static IReadOnlyList<MemberOption> GetByIds(IEnumerable<string> ids)
    {
        var idSet = ids.ToHashSet();
        return Members.Where(member => idSet.Contains(member.Id)).ToList();
    }

    public record MemberOption(string Id, string Name);
}
