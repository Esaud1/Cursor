using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using WComtismc.Models;

namespace WComtismc.Services;

public class UserStore : IUserStore
{
    private readonly ConcurrentDictionary<string, UserAccount> _usersByEmail = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<Guid, UserAccount> _usersById = new();

    public UserAccount? GetByEmail(string email) =>
        _usersByEmail.TryGetValue(email, out var user) ? user : null;

    public UserAccount? GetById(Guid id) =>
        _usersById.TryGetValue(id, out var user) ? user : null;

    public UserAccount Register(string fullName, string email, string password)
    {
        if (_usersByEmail.ContainsKey(email))
        {
            throw new InvalidOperationException("البريد الإلكتروني مسجل مسبقاً.");
        }

        var user = new UserAccount
        {
            Id = Guid.NewGuid(),
            FullName = fullName.Trim(),
            Email = email.Trim(),
            PasswordHash = HashPassword(password),
            CreatedAt = DateTime.UtcNow
        };

        _usersByEmail[user.Email] = user;
        _usersById[user.Id] = user;
        return user;
    }

    public bool ValidateCredentials(string email, string password)
    {
        var user = GetByEmail(email);
        return user != null && user.PasswordHash == HashPassword(password);
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }
}
