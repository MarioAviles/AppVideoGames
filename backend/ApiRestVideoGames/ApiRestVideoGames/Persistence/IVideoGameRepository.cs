using ApiRestVideoGames.Models;

namespace ApiRestVideoGames.Persistence
{
    public interface IVideoGameRepository
    {
        Task<IEnumerable<VideoGame>> GetAllVideoGamesAsync();
        Task<VideoGame?> GetVideoGameAsync(int id);
        Task AddVideoGameAsync(VideoGame entity);
        Task UpdateVideoGameAsync(VideoGame entity);
        Task DeleteVideoGameAsync(int id);
    }
}
