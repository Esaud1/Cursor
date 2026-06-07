using EnadWebApp.Models;

namespace EnadWebApp.Services;

public interface ISessionStore
{
    IReadOnlyList<SessionRecord> GetAll();

    SessionRecord? GetById(Guid id);

    void Add(SessionRecord session);
}
