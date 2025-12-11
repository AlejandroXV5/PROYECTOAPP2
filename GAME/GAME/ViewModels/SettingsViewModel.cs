using System.ComponentModel;
using System.Windows.Input;
using GAME.Services;

namespace GAME.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public LocalizationManager Localization => LocalizationManager.Instance;

        public ICommand SetEnglishCommand { get; }
        public ICommand SetSpanishCommand { get; }

        public SettingsViewModel()
        {
            LocalizationManager.Instance.PropertyChanged += OnLocalizationPropertyChanged;

            SetEnglishCommand = new Command(() => Localization.SetLanguage("en"));
            SetSpanishCommand = new Command(() => Localization.SetLanguage("es"));
        }

        private void OnLocalizationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Localization));
        }
    }
}
