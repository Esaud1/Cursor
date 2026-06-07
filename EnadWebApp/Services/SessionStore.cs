using System.Collections.Concurrent;
using EnadWebApp.Models;

namespace EnadWebApp.Services;

public class SessionStore : ISessionStore
{
    private readonly ConcurrentDictionary<Guid, SessionRecord> _sessions = new();

    public IReadOnlyList<SessionRecord> GetAll() =>
        _sessions.Values
            .OrderByDescending(session => session.SessionDate)
            .ThenByDescending(session => session.CreatedAt)
            .ToList();

    public SessionRecord? GetById(Guid id) =>
        _sessions.TryGetValue(id, out var session) ? session : null;

    public void Add(SessionRecord session) =>
        _sessions[session.Id] = session;
}
