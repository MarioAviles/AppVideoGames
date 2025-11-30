using ApiRestVideoGames.Models;

namespace ApiRestVideoGames.Persistence
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
        {
            new User { Username = "juan", Password = "1234" },
            new User { Username = "admin", Password = "admin" }
        };

        public User? GetUser(string username, string password)
        {
            return _users.FirstOrDefault(u =>
                u.Username == username && u.Password == password);
        }
    }
}
