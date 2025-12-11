using JuegoPRU.ViewModels;
using JuegoPRU.Models;

namespace JuegoPRU.Views
{
    public partial class CharacterSelectionPage : ContentPage
    {
        private CharacterSelectionViewModel _viewModel;

        public CharacterSelectionPage(CharacterSelectionViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Reset();
        }
    }
}
