using GAME.ViewModels;

namespace GAME.Views;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage()
    {
        InitializeComponent();
        BindingContext = new StatisticsViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is StatisticsViewModel viewModel)
        {
            await viewModel.LoadStatistics();
        }
    }
}