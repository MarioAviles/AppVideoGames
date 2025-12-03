using ApiRestVideoGames.Models;

namespace ApiRestVideoGames.Persistence
{
    public interface IUserRepository
    {
        User? GetUser(string username, string password);

    }
}
