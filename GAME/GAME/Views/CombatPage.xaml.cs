using GAME.ViewModels;
using GAME.Models;

namespace GAME.Views
{
    [QueryProperty(nameof(Player1Name), "player1")]
    [QueryProperty(nameof(Player2Name), "player2")]
    [QueryProperty(nameof(P1RaceStr), "p1race")]
    [QueryProperty(nameof(P1WeaponStr), "p1weapon")]
    [QueryProperty(nameof(P2RaceStr), "p2race")]
    [QueryProperty(nameof(P2WeaponStr), "p2weapon")]
    public partial class CombatPage : ContentPage
    {
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string P1RaceStr { get; set; }
        public string P1WeaponStr { get; set; }
        public string P2RaceStr { get; set; }
        public string P2WeaponStr { get; set; }

        private CombatViewModel _viewModel;

        public CombatPage(CombatViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            if (!string.IsNullOrEmpty(Player1Name) && !string.IsNullOrEmpty(Player2Name) &&
                Enum.TryParse<RaceType>(P1RaceStr, out var p1Race) &&
                Enum.TryParse<WeaponType>(P1WeaponStr, out var p1Weapon) &&
                Enum.TryParse<RaceType>(P2RaceStr, out var p2Race) &&
                Enum.TryParse<WeaponType>(P2WeaponStr, out var p2Weapon))
            {
                var player1 = new Character(p1Race, p1Weapon, Player1Name);
                var player2 = new Character(p2Race, p2Weapon, Player2Name);

                _viewModel.Initialize(player1, player2);
            }
        }
    }
}
