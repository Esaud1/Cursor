using WComtismc.Models;

namespace WComtismc.Services;

public interface IUserStore
{
    UserAccount? GetByEmail(string email);

    UserAccount? GetById(Guid id);

    UserAccount Register(string fullName, string email, string password);

    bool ValidateCredentials(string email, string password);
}
