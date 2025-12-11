using SQLite;

namespace GAME.Models
{
    [Table("players")]
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Name { get; set; }

        public int Wins { get; set; } = 0;

        public int Losses { get; set; } = 0;

        public int Draws { get; set; } = 0;

        public int GamesPlayed { get; set; } = 0;

        [Ignore]
        public int TotalScore => Wins * 3 + Draws;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
