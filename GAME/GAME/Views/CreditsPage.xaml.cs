using GAME.ViewModels;

namespace GAME.Views;

public partial class CreditsPage : ContentPage
{
    public CreditsPage()
    {
        InitializeComponent();
        BindingContext = new CreditsViewModel();
    }
}