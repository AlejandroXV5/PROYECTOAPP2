namespace JuegoPRU.Models
{
    public class GameState
    {
        public Character Player1 { get; set; }
        public Character Player2 { get; set; }
        public int CurrentPlayerTurn { get; set; } = 1; // 1 or 2
        public int TurnCount { get; set; } = 0;
        public List<string> ActionHistory { get; set; } = new List<string>();
        public bool IsGameOver { get; set; } = false;
        public int? WinnerId { get; set; } = null; // 1 or 2, null if draw

        public GameState()
        {
        }

        public GameState(Character player1, Character player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public Character GetCurrentPlayer()
        {
            return CurrentPlayerTurn == 1 ? Player1 : Player2;
        }

        public Character GetOpponentPlayer()
        {
            return CurrentPlayerTurn == 1 ? Player2 : Player1;
        }

        public void SwitchTurn()
        {
            CurrentPlayerTurn = CurrentPlayerTurn == 1 ? 2 : 1;
            TurnCount++;
        }

        public void AddAction(string action)
        {
            ActionHistory.Add($"Turn {TurnCount}: {action}");
        }

        public void CheckGameOver()
        {
            if (!Player1.IsAlive && !Player2.IsAlive)
            {
                IsGameOver = true;
                WinnerId = null; // Draw
            }
            else if (!Player1.IsAlive)
            {
                IsGameOver = true;
                WinnerId = 2;
            }
            else if (!Player2.IsAlive)
            {
                IsGameOver = true;
                WinnerId = 1;
            }
        }

        public string GetGameOverMessage()
        {
            if (WinnerId == null)
                return "Draw!";
            else if (WinnerId == 1)
                return $"{Player1.Name} wins!";
            else
                return $"{Player2.Name} wins!";
        }
    }
}
