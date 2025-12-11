using JuegoPRU.Views;

namespace JuegoPRU
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("characterselection", typeof(CharacterSelectionPage));
            Routing.RegisterRoute("combat", typeof(CombatPage));
            Routing.RegisterRoute("statistics", typeof(StatisticsPage));
            Routing.RegisterRoute("howtoplay", typeof(HowToPlayPage));
            Routing.RegisterRoute("technicalinfo", typeof(TechnicalInfoPage));
            Routing.RegisterRoute("credits", typeof(CreditsPage));
            Routing.RegisterRoute("settings", typeof(SettingsPage));
            Routing.RegisterRoute("rules", typeof(RulesPage));
        }
    }
}
