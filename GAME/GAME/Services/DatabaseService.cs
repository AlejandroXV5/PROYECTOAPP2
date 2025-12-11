using SQLite;
using GAME.Models;

namespace GAME.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "gamedata.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            
            // Crear tablas si no existen
            _database.CreateTableAsync<Player>().Wait();
            _database.CreateTableAsync<MatchHistory>().Wait();
        }

        // Métodos para jugadores
        public Task<List<Player>> GetAllPlayers()
        {
            return _database.Table<Player>().ToListAsync();
        }

        public Task<Player> GetPlayerByName(string name)
        {
            return _database.Table<Player>().Where(p => p.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> AddPlayer(Player player)
        {
            return _database.InsertAsync(player);
        }

        public Task<int> UpdatePlayer(Player player)
        {
            return _database.UpdateAsync(player);
        }

        public Task<int> DeletePlayer(Player player)
        {
            return _database.DeleteAsync(player);
        }

        // Métodos para historial de partidas
        public Task<List<MatchHistory>> GetAllMatches()
        {
            return _database.Table<MatchHistory>().OrderByDescending(m => m.PlayedAt).ToListAsync();
        }

        public Task<int> AddMatch(MatchHistory match)
        {
            return _database.InsertAsync(match);
        }

        public Task<int> DeleteMatch(MatchHistory match)
        {
            return _database.DeleteAsync(match);
        }

        public Task<int> DeleteAllPlayers()
        {
            return _database.DeleteAllAsync<Player>();
        }

        public Task<int> DeleteAllMatches()
        {
            return _database.DeleteAllAsync<MatchHistory>();
        }
    }
}
