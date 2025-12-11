using System.ComponentModel;
using GAME.Services;

namespace GAME.ViewModels
{
    public class CreditsViewModel : BaseViewModel
    {
        private string _creditsTitle;
        private string _creditsDev;
        private string _creditsTech;

        public string CreditsTitle
        {
            get => _creditsTitle;
            set => SetProperty(ref _creditsTitle, value);
        }

        public string CreditsDev
        {
            get => _creditsDev;
            set => SetProperty(ref _creditsDev, value);
        }

        public string CreditsTech
        {
            get => _creditsTech;
            set => SetProperty(ref _creditsTech, value);
        }

        public CreditsViewModel()
        {
            UpdateLocalizedStrings();
            LocalizationManager.Instance.PropertyChanged += OnLocalizationPropertyChanged;
        }

        private void UpdateLocalizedStrings()
        {
            CreditsTitle = LocalizationManager.Instance.CreditsTitle;
            CreditsDev = LocalizationManager.Instance.CreditsDev;
            CreditsTech = LocalizationManager.Instance.CreditsTech;
        }

        private void OnLocalizationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateLocalizedStrings();
        }
    }
}