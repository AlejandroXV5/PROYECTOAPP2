using System.Windows.Input;
using JuegoPRU.Services;

namespace JuegoPRU.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public LocalizationManager Localization => LocalizationManager.Instance;

        public ICommand SetEnglishCommand { get; }
        public ICommand SetSpanishCommand { get; }

        public SettingsViewModel()
        {
            SetEnglishCommand = new Command(() => Localization.SetLanguage("en"));
            SetSpanishCommand = new Command(() => Localization.SetLanguage("es"));
        }
    }
}
