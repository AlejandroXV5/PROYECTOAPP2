using GAME.ViewModels;

namespace GAME
{
    public partial class App : Application
    {
        private readonly CombatViewModel _combatViewModel;

        public App(CombatViewModel combatViewModel)
        {
            InitializeComponent();
            _combatViewModel = combatViewModel;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            _combatViewModel?.StopMusic();
        }
    }
}