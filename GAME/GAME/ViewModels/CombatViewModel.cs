using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Maui.Audio;
using GAME.Models;
using GAME.Services;

namespace GAME.ViewModels
{
    public class CombatViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private readonly IAudioManager _audioManager;
        private IAudioPlayer _audioPlayer;
        private GameState _gameState;
        private ObservableCollection<string> _battleLog;
        private string _currentPlayerName;
        private int _player1Health;
        private int _player2Health;
        private int _distance;
        private bool _isGameOver;
        private string _gameOverMessage;
        private bool _isPlayer1Turn;
        private string _currentCharacterImage;
        private string _player1DisplayName;
        private string _player2DisplayName;
        private string _player1Image;
        private string _player2Image;

        public GameState GameState
        {
            get => _gameState;
            set => SetProperty(ref _gameState, value);
        }

        public ObservableCollection<string> BattleLog
        {
            get => _battleLog;
            set => SetProperty(ref _battleLog, value);
        }

        public string CurrentPlayerName
        {
            get => _currentPlayerName;
            set
            {
                if (SetProperty(ref _currentPlayerName, value))
                {
                    OnPropertyChanged(nameof(TurnIndicatorText));
                }
            }
        }

        public int Player1Health
        {
            get => _player1Health;
            set
            {
                if (SetProperty(ref _player1Health, value))
                {
                    OnPropertyChanged(nameof(Player1HPText));
                }
            }
        }

        public int Player2Health
        {
            get => _player2Health;
            set
            {
                if (SetProperty(ref _player2Health, value))
                {
                    OnPropertyChanged(nameof(Player2HPText));
                }
            }
        }

        public int Distance
        {
            get => _distance;
            set
            {
                if (SetProperty(ref _distance, value))
                {
                    OnPropertyChanged(nameof(DistanceText));
                }
            }
        }

        public string DistanceText => string.Format(LocalizationManager.Instance.DistanceLabel, Distance);
        public string TurnIndicatorText => string.Format(LocalizationManager.Instance.TurnIndicator, CurrentPlayerName);
        public string Player1LabelText => string.Format(LocalizationManager.Instance.PlayerLabel, "1", Player1DisplayName);
        public string Player2LabelText => string.Format(LocalizationManager.Instance.PlayerLabel, "2", Player2DisplayName);
        public string Player1HPText => string.Format(LocalizationManager.Instance.HPLabel, Player1Health);
        public string Player2HPText => string.Format(LocalizationManager.Instance.HPLabel, Player2Health);

        public bool IsGameOver
        {
            get => _isGameOver;
            set => SetProperty(ref _isGameOver, value);
        }

        public string GameOverMessage
        {
            get => _gameOverMessage;
            set => SetProperty(ref _gameOverMessage, value);
        }

        public bool IsPlayer1Turn
        {
            get => _isPlayer1Turn;
            set => SetProperty(ref _isPlayer1Turn, value);
        }

        public string CurrentCharacterImage
        {
            get => _currentCharacterImage;
            set => SetProperty(ref _currentCharacterImage, value);
        }

        public string Player1DisplayName
        {
            get => _player1DisplayName;
            set
            {
                if (SetProperty(ref _player1DisplayName, value))
                {
                    OnPropertyChanged(nameof(Player1LabelText));
                }
            }
        }

        public string Player2DisplayName
        {
            get => _player2DisplayName;
            set
            {
                if (SetProperty(ref _player2DisplayName, value))
                {
                    OnPropertyChanged(nameof(Player2LabelText));
                }
            }
        }

        public string Player1Image
        {
            get => _player1Image;
            set => SetProperty(ref _player1Image, value);
        }

        public string Player2Image
        {
            get => _player2Image;
            set => SetProperty(ref _player2Image, value);
        }

        public ICommand AttackCommand { get; }
        public ICommand HealCommand { get; }
        public ICommand AdvanceCommand { get; }
        public ICommand RetreatCommand { get; }
        public ICommand SurrenderCommand { get; }
        public ICommand ReturnToMenuCommand { get; }

        public CombatViewModel(IAudioManager audioManager)
        {
            _audioManager = audioManager;
            _databaseService = new DatabaseService();
            BattleLog = new ObservableCollection<string>();

            AttackCommand = new Command(async () => await ExecuteAttack());
            HealCommand = new Command(async () => await ExecuteHeal());
            AdvanceCommand = new Command(async () => await ExecuteAdvance());
            RetreatCommand = new Command(async () => await ExecuteRetreat());
            SurrenderCommand = new Command(async () => await ExecuteSurrender());
            ReturnToMenuCommand = new Command(async () => await ReturnToMenu());

            LocalizationManager.Instance.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(DistanceText));
                OnPropertyChanged(nameof(TurnIndicatorText));
                OnPropertyChanged(nameof(Player1LabelText));
                OnPropertyChanged(nameof(Player2LabelText));
                OnPropertyChanged(nameof(Player1HPText));
                OnPropertyChanged(nameof(Player2HPText));
            };
        }

        public void Initialize(Character player1, Character player2)
        {
            GameState = new GameState(player1, player2);
            Distance = 5;
            Player1DisplayName = player1.Name;
            Player2DisplayName = player2.Name;
            Player1Image = GetCharacterImage(player1.Race);
            Player2Image = GetCharacterImage(player2.Race);
            UpdateUI();
            AddLog(string.Format(LocalizationManager.Instance.CombatStartedMessage, player1.Name, player2.Name));
            PlayMusic();
        }

        private async void PlayMusic()
        {
            try
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("combat_music.mp3");
                _audioPlayer = _audioManager.CreatePlayer(stream);
                _audioPlayer.Loop = true;
                _audioPlayer.Play();
            }
            catch (Exception ex)
            {
                // Manejar error si el archivo no existe o hay problemas de audio
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }

        private void UpdateUI()
        {
            Player1Health = GameState.Player1.CurrentHealth;
            Player2Health = GameState.Player2.CurrentHealth;
            CurrentPlayerName = GameState.GetCurrentPlayer().Name;
            IsPlayer1Turn = GameState.CurrentPlayerTurn == 1;
            CurrentCharacterImage = GameState.CurrentPlayerTurn == 1 ? Player1Image : Player2Image;
        }

        private async Task ExecuteAttack()
        {
            if (IsGameOver) return;

            var attacker = GameState.GetCurrentPlayer();
            var defender = GameState.GetOpponentPlayer();
            var loc = LocalizationManager.Instance;

            // Apply bleeding at start of turn
            attacker.ApplyBleeding();
            if (attacker.BleeedingTurnsRemaining > 0)
            {
                AddLog(string.Format(loc.BleedingDamageMessage, attacker.Name, attacker.BleeedingDamagePerTurn));
            }

            // Check if can attack based on distance
            if (Distance > 0 && !CanAttackFromDistance(attacker.Weapon))
            {
                await Application.Current.MainPage.DisplayAlert(loc.CannotAttackTitle, loc.CannotAttackMessage, loc.OKButton);
                return;
            }

            int damage = attacker.GetAttackDamage(Distance);
            defender.TakeDamage(damage);

            AddLog(string.Format(loc.AttackMessage, attacker.Name, defender.Name, damage));

            // Apply bleeding if applicable
            if (attacker.Weapon == WeaponType.Axe)
            {
                defender.BleeedingTurnsRemaining = 2;
                defender.BleeedingDamagePerTurn = 3;
                AddLog(string.Format(loc.IsBleedingMessage, defender.Name));
            }

            GameState.AddAction($"{attacker.Name} attacked for {damage} damage"); // Internal action log
            GameState.CheckGameOver();

            if (GameState.IsGameOver)
            {
                await EndGame();
            }
            else
            {
                GameState.SwitchTurn();
                UpdateUI();
            }
        }

        private async Task ExecuteHeal()
        {
            if (IsGameOver) return;

            var healer = GameState.GetCurrentPlayer();
            var loc = LocalizationManager.Instance;
            int healAmount = healer.GetHealAmount();
            healer.Heal(healAmount);

            AddLog(string.Format(loc.HealMessage, healer.Name, healAmount));
            GameState.AddAction($"{healer.Name} healed for {healAmount} HP"); // Internal action log

            GameState.SwitchTurn();
            UpdateUI();
        }

        private async Task ExecuteAdvance()
        {
            if (IsGameOver) return;

            var loc = LocalizationManager.Instance;
            if (Distance > 0)
            {
                Distance--;
                AddLog(string.Format(loc.AdvanceMessage, GameState.GetCurrentPlayer().Name, Distance));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(loc.AlreadyCloseTitle, loc.AlreadyCloseMessage, loc.OKButton);
                return;
            }

            GameState.SwitchTurn();
            UpdateUI();
        }

        private async Task ExecuteRetreat()
        {
            if (IsGameOver) return;

            var loc = LocalizationManager.Instance;
            Distance++;
            AddLog(string.Format(loc.RetreatMessage, GameState.GetCurrentPlayer().Name, Distance));
            GameState.SwitchTurn();
            UpdateUI();
        }

        private async Task ExecuteSurrender()
        {
            if (IsGameOver) return;

            var surrendering = GameState.CurrentPlayerTurn;
            var winnerId = surrendering == 1 ? 2 : 1;
            GameState.WinnerId = winnerId;
            GameState.IsGameOver = true;

            var loc = LocalizationManager.Instance;
            AddLog(string.Format(loc.SurrenderMessage, GameState.GetCurrentPlayer().Name));

            await EndGame();
        }

        private bool CanAttackFromDistance(WeaponType weapon)
        {
            return weapon == WeaponType.SniperRifle ||
                   weapon == WeaponType.FireStaff ||
                   weapon == WeaponType.EarthStaff ||
                   weapon == WeaponType.AirStaff ||
                   weapon == WeaponType.WaterStaff;
        }

        public void StopMusic()
        {
            _audioPlayer?.Stop();
            _audioPlayer?.Dispose();
            _audioPlayer = null;
        }

        private async Task EndGame()
        {
            IsGameOver = true;

            var loc = LocalizationManager.Instance;
            if (GameState.WinnerId == null)
            {
                GameOverMessage = loc.DrawMessage;
            }
            else if (GameState.WinnerId == 1)
            {
                GameOverMessage = string.Format(loc.WinnerMessage, GameState.Player1.Name);
            }
            else
            {
                GameOverMessage = string.Format(loc.WinnerMessage, GameState.Player2.Name);
            }

            // Save match history
            var winner = GameState.WinnerId == 1 ? GameState.Player1.Name :
                         GameState.WinnerId == 2 ? GameState.Player2.Name : null;

            var match = new MatchHistory
            {
                Player1Name = GameState.Player1.Name,
                Player2Name = GameState.Player2.Name,
                WinnerName = winner,
                ResultType = GameState.WinnerId == null ? "draw" : "win", // Internal identifiers, not user-facing strings
                Player1FinalHealth = GameState.Player1.CurrentHealth,
                Player2FinalHealth = GameState.Player2.CurrentHealth,
                TurnsPlayed = GameState.TurnCount
            };

            await _databaseService.AddMatch(match);

            // Update player statistics
            var player1 = await _databaseService.GetPlayerByName(GameState.Player1.Name);
            var player2 = await _databaseService.GetPlayerByName(GameState.Player2.Name);

            if (player1 == null)
            {
                player1 = new Player { Name = GameState.Player1.Name };
                await _databaseService.AddPlayer(player1);
            }

            if (player2 == null)
            {
                player2 = new Player { Name = GameState.Player2.Name };
                await _databaseService.AddPlayer(player2);
            }

            if (GameState.WinnerId == 1)
            {
                player1.Wins++;
                player2.Losses++;
            }
            else if (GameState.WinnerId == 2)
            {
                player2.Wins++;
                player1.Losses++;
            }
            else
            {
                player1.Draws++;
                player2.Draws++;
            }

            player1.GamesPlayed++;
            player2.GamesPlayed++;

            await _databaseService.UpdatePlayer(player1);
            await _databaseService.UpdatePlayer(player2);
        }

        private async Task ReturnToMenu()
        {
            StopMusic();
            await Shell.Current.GoToAsync("//main");
        }

        private string GetCharacterImage(RaceType race)
        {
            var imageName = race == RaceType.Beast ? "best" : race.ToString().ToLower();
            return imageName;
        }

        private void AddLog(string message)
        {
            BattleLog.Insert(0, string.Format(LocalizationManager.Instance.TurnLogFormat, GameState.TurnCount, message));
        }
    }
}
