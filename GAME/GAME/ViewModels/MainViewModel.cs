using System.Collections.ObjectModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GAME.Models;
using GAME.Services;

namespace GAME.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public LocalizationManager Localization => LocalizationManager.Instance;

        public ICommand StartGameCommand { get; }
        public ICommand ViewStatsCommand { get; }
        public ICommand HowToPlayCommand { get; }
        public ICommand TechnicalInfoCommand { get; }
        public ICommand CreditsCommand { get; }
        public ICommand SettingsCommand { get; }

        public MainViewModel()
        {
            LocalizationManager.Instance.PropertyChanged += OnLocalizationPropertyChanged;

            StartGameCommand = new Command(async () => await GoToCharacterSelection());
            ViewStatsCommand = new Command(async () => await GoToStatistics());
            HowToPlayCommand = new Command(async () => await GoToHowToPlay());
            TechnicalInfoCommand = new Command(async () => await GoToTechnicalInfo());
            CreditsCommand = new Command(async () => await GoToCredits());
            SettingsCommand = new Command(async () => await Shell.Current.GoToAsync("settings"));
        }

        private void OnLocalizationPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Localization));
        }

        private async Task GoToCharacterSelection()
        {
            // Placeholder for actual navigation logic
            await Shell.Current.GoToAsync("characterselection");
        }

        private async Task GoToStatistics()
        {
            await Shell.Current.GoToAsync("statistics");
        }

        private async Task GoToHowToPlay()
        {
            await Shell.Current.GoToAsync("howtoplay");
        }

        private async Task GoToTechnicalInfo()
        {
            await Shell.Current.GoToAsync("technicalinfo");
        }

        private async Task GoToCredits()
        {
            await Shell.Current.GoToAsync("credits");
        }
    }
}
