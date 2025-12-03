using System.Data;
using ApiRestVideoGames.Models;
using MySql.Data.MySqlClient;

namespace ApiRestVideoGames.Persistence
{
    public class MySqlVideoGameRepository : IVideoGameRepository
    {
        private readonly string _connectionString;

        public MySqlVideoGameRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MySqlConnection");
        }

        public async Task<IEnumerable<VideoGame>> GetAllVideoGamesAsync()
        {
            var list = new List<VideoGame>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new MySqlCommand("SELECT * FROM VideoGames", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new VideoGame
                {
                    Rank = reader.GetInt32("Rank"),
                    Name = reader["Name"]?.ToString(),
                    Platform = reader["Platform"]?.ToString(),
                    Year = reader["Year"] == DBNull.Value ? null : Convert.ToInt32(reader["Year"]),
                    Genre = reader["Genre"]?.ToString(),
                    Publisher = reader["Publisher"]?.ToString(),

                    NA_Sales = reader["NA_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["NA_Sales"]),
                    EU_Sales = reader["EU_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["EU_Sales"]),
                    JP_Sales = reader["JP_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["JP_Sales"]),
                    Other_Sales = reader["Other_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Other_Sales"]),
                    Global_Sales = reader["Global_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Global_Sales"])

                });
            }

            return list;
        }

        public async Task<VideoGame?> GetVideoGameAsync(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new MySqlCommand("SELECT * FROM VideoGames WHERE Rank=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new VideoGame
                {
                    Rank = reader.GetInt32("Rank"),
                    Name = reader["Name"]?.ToString(),
                    Platform = reader["Platform"]?.ToString(),
                    Year = reader["Year"] == DBNull.Value ? null : Convert.ToInt32(reader["Year"]),
                    Genre = reader["Genre"]?.ToString(),
                    Publisher = reader["Publisher"]?.ToString(),

                    NA_Sales = reader["NA_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["NA_Sales"]),
                    EU_Sales = reader["EU_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["EU_Sales"]),
                    JP_Sales = reader["JP_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["JP_Sales"]),
                    Other_Sales = reader["Other_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Other_Sales"]),
                    Global_Sales = reader["Global_Sales"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Global_Sales"])

                };
            }

            return null;
        }

        public async Task AddVideoGameAsync(VideoGame entity)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"INSERT INTO VideoGames 
            (`Rank`, `Name`, `Platform`, `Year`, `Genre`, `Publisher`, 
             `NA_Sales`, `EU_Sales`, `JP_Sales`, `Other_Sales`, `Global_Sales`)
            VALUES (@Rank, @Name, @Platform, @Year, @Genre, @Publisher,
                    @NA_Sales, @EU_Sales, @JP_Sales, @Other_Sales, @Global_Sales)";


            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Rank", entity.Rank);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Platform", entity.Platform);
            cmd.Parameters.AddWithValue("@Year", entity.Year);
            cmd.Parameters.AddWithValue("@Genre", entity.Genre);
            cmd.Parameters.AddWithValue("@Publisher", entity.Publisher);
            cmd.Parameters.AddWithValue("@NA_Sales", entity.NA_Sales);
            cmd.Parameters.AddWithValue("@EU_Sales", entity.EU_Sales);
            cmd.Parameters.AddWithValue("@JP_Sales", entity.JP_Sales);
            cmd.Parameters.AddWithValue("@Other_Sales", entity.Other_Sales);
            cmd.Parameters.AddWithValue("@Global_Sales", entity.Global_Sales);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateVideoGameAsync(VideoGame entity)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"UPDATE VideoGames SET
                `Name`=@Name, 
                `Platform`=@Platform, 
                `Year`=@Year, 
                `Genre`=@Genre, 
                `Publisher`=@Publisher,
                `NA_Sales`=@NA_Sales, 
                `EU_Sales`=@EU_Sales, 
                `JP_Sales`=@JP_Sales,
                `Other_Sales`=@Other_Sales, 
                `Global_Sales`=@Global_Sales
            WHERE `Rank`=@Rank";


            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Rank", entity.Rank);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Platform", entity.Platform);
            cmd.Parameters.AddWithValue("@Year", entity.Year);
            cmd.Parameters.AddWithValue("@Genre", entity.Genre);
            cmd.Parameters.AddWithValue("@Publisher", entity.Publisher);
            cmd.Parameters.AddWithValue("@NA_Sales", entity.NA_Sales);
            cmd.Parameters.AddWithValue("@EU_Sales", entity.EU_Sales);
            cmd.Parameters.AddWithValue("@JP_Sales", entity.JP_Sales);
            cmd.Parameters.AddWithValue("@Other_Sales", entity.Other_Sales);
            cmd.Parameters.AddWithValue("@Global_Sales", entity.Global_Sales);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteVideoGameAsync(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new MySqlCommand("DELETE FROM VideoGames WHERE Rank=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
