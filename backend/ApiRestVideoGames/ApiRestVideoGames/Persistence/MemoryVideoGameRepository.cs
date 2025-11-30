using ApiRestVideoGames.Models;

namespace ApiRestVideoGames.Persistence
{
    public class MemoryVideoGameRepository : IVideoGameRepository
    {
        private static List<VideoGame> _data = new();

        public Task<IEnumerable<VideoGame>> GetAllVideoGamesAsync()
        {
            return Task.FromResult(_data.AsEnumerable());
        }

        public Task<VideoGame?> GetVideoGameAsync(int id)
        {
            var item = _data.FirstOrDefault(x => x.Rank == id);
            return Task.FromResult(item);
        }

        public Task AddVideoGameAsync(VideoGame entity)
        {
            _data.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateVideoGameAsync(VideoGame entity)
        {
            var index = _data.FindIndex(x => x.Rank == entity.Rank);
            if (index >= 0)
                _data[index] = entity;

            return Task.CompletedTask;
        }

        public Task DeleteVideoGameAsync(int id)
        {
            _data.RemoveAll(x => x.Rank == id);
            return Task.CompletedTask;
        }
    }
}
