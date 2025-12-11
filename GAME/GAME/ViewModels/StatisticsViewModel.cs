using System.Collections.ObjectModel;
using System.Windows.Input;
using GAME.Models;
using GAME.Services;

namespace GAME.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Player> _topPlayers;
        private ObservableCollection<MatchHistory> _recentMatches;
        private bool _isLoading;

        public ObservableCollection<Player> TopPlayers
        {
            get => _topPlayers;
            set => SetProperty(ref _topPlayers, value);
        }

        public ObservableCollection<MatchHistory> RecentMatches
        {
            get => _recentMatches;
            set => SetProperty(ref _recentMatches, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand LoadStatisticsCommand { get; }
        public ICommand ReturnToMenuCommand { get; }

        public StatisticsViewModel()
        {
            _databaseService = new DatabaseService();
            TopPlayers = new ObservableCollection<Player>();
            RecentMatches = new ObservableCollection<MatchHistory>();

            LoadStatisticsCommand = new Command(async () => await LoadStatistics());
            ReturnToMenuCommand = new Command(async () => await ReturnToMenu());
        }

        private async Task LoadStatistics()
        {
            try
            {
                IsLoading = true;

                // Load top players
                var players = await _databaseService.GetAllPlayers();
                var sortedPlayers = players.OrderByDescending(p => p.TotalScore).Take(10).ToList();

                TopPlayers.Clear();
                foreach (var player in sortedPlayers)
                {
                    TopPlayers.Add(player);
                }

                // Load recent matches
                var matches = await _databaseService.GetAllMatches();
                var recentMatches = matches.Take(20).ToList();

                RecentMatches.Clear();
                foreach (var match in recentMatches)
                {
                    RecentMatches.Add(match);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load statistics: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ReturnToMenu()
        {
            await Shell.Current.GoToAsync("//main");
        }
    }
}
