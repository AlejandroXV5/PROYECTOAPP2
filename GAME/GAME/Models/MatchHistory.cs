using SQLite;

namespace JuegoPRU.Models
{
    [Table("match_history")]
    public class MatchHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }

        public string WinnerName { get; set; } // null for draws

        public string ResultType { get; set; } // "win", "loss", "draw"

        public DateTime PlayedAt { get; set; } = DateTime.Now;

        public int Player1FinalHealth { get; set; }

        public int Player2FinalHealth { get; set; }

        public int TurnsPlayed { get; set; }
    }
}
