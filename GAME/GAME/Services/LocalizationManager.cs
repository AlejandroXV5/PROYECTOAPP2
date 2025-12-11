using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GAME.Services
{
    public class LocalizationManager : INotifyPropertyChanged
    {
        private static LocalizationManager _instance;
        private string _currentLanguage = "en";

        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, Dictionary<string, string>> _localizedStrings = new Dictionary<string, Dictionary<string, string>>
        {
            ["en"] = new Dictionary<string, string>
            {
                // Main Menu
                ["StartGameText"] = "Start Game",
                ["ViewStatsText"] = "Statistics",
                ["HowToPlayText"] = "How to Play",
                ["TechnicalInfoText"] = "Technical Info",
                ["CreditsText"] = "Credits",
                ["SettingsText"] = "Settings",
               
                // Character Selection
                ["Player1NameLabel"] = "Player 1 Name",
                ["Player2NameLabel"] = "Player 2 Name",
                ["EnterPlayer1NamePlaceholder"] = "Enter Player 1 name...",
                ["EnterPlayer2NamePlaceholder"] = "Enter Player 2 name...",
                ["SelectWeaponLabel"] = "Select Weapon",
                ["SelectRaceLabel"] = "Select Race",
                ["PlayButtonText"] = "Play",
                ["RaceElf"] = "Elf",
                ["RaceHuman"] = "Human",
                ["RaceDwarf"] = "Dwarf",
                ["RaceOrc"] = "Orc",
                ["RaceTroll"] = "Troll",
                ["RaceBeast"] = "Beast",
                ["RaceDwarf"] = "Dwarf",
                ["RaceTroll"] = "Troll",
                ["HumanName"] = "Human",
                ["HumanDescription"] = "Balanced race with ranged weapons",
                ["ElfName"] = "Elf",
                ["ElfDescription"] = "Magic user with elemental staffs",
                ["OrcName"] = "Orc",
                ["OrcDescription"] = "Strong race with heavy weapons",
                ["BeastName"] = "Beast",
                ["BeastDescription"] = "Fierce fighter with close combat skills",
                ["DwarfName"] = "Dwarf",
                ["DwarfDescription"] = "Sturdy warrior with defensive abilities",
                ["TrollName"] = "Troll",
                ["TrollDescription"] = "Regenerating tank with high health",
                ["SwordName"] = "Sword",
                ["AxeName"] = "Axe",
                ["BowName"] = "Bow",
                ["SniperRifleName"] = "Sniper Rifle",
                ["FireStaffName"] = "Fire Staff",
                ["EarthStaffName"] = "Earth Staff",
                ["WaterStaffName"] = "Water Staff",
                ["AirStaffName"] = "Air Staff",
                ["SwordDesc"] = "Balanced melee weapon",
                ["AxeDesc"] = "Heavy weapon, causes bleeding",
                ["BowDesc"] = "Ranged weapon",
                ["SniperRifleDesc"] = "Long range precision weapon",
                ["FireStaffDesc"] = "Fire magic, ranged attacks",
                ["EarthStaffDesc"] = "Earth magic, defensive",
                ["WaterStaffDesc"] = "Water magic, healing bonus",
                ["AirStaffDesc"] = "Air magic, speed attacks",
                ["ShotgunName"] = "Shotgun",
                ["ShotgunDesc"] = "Powerful close-range firearm",
                ["HammerName"] = "Hammer",
                ["HammerDesc"] = "Heavy weapon that stuns enemies",
                ["FistsName"] = "Fists",
                ["FistsDesc"] = "Natural weapons, fast attacks",
                
                // Button Texts
                ["NextPlayerButtonText"] = "Next Player",
                ["StartCombatButtonText"] = "Start Combat",
                
                // Instructions
                ["SelectRaceInstruction"] = "{0}, select your race",
                ["SelectWeaponInstruction"] = "{0}, select your weapon",
                
                // Error Messages
                ["ErrorPlayer1Selection"] = "Player 1 must complete their selection",
                ["ErrorPlayer2Selection"] = "Player 2 must complete their selection",
                ["ErrorEnterNames"] = "Please enter names for both players",
                ["ErrorDifferentNames"] = "Players must have different names",
                ["ErrorCompleteSelection"] = "Please complete all selections",
                ["ErrorTitle"] = "Error",
                ["ErrorMessage"] = "Please select both races and weapons before playing.",
                
                // Combat
                ["DistanceLabel"] = "Distance: {0}",
                ["TurnIndicator"] = "Turn: {0}",
                ["HPLabel"] = "HP: {0}",
                ["PlayerLabel"] = "Player {0}: {1}",
                ["AttackButtonText"] = "Attack",
                ["HealButtonText"] = "Heal",
                ["AdvanceButtonText"] = "Advance",
                ["RetreatButtonText"] = "Retreat",
                ["SurrenderButtonText"] = "Abandon",
                ["ReturnToMenuButtonText"] = "Return to Menu",
                ["GameOverText"] = "Game Over",
                ["CombatStartedMessage"] = "Combat started between {0} and {1}!",
                ["AttackMessage"] = "{0} attacks {1} for {2} damage!",
                ["HealMessage"] = "{0} heals for {1} HP!",
                ["AdvanceMessage"] = "{0} advances! Distance: {1}",
                ["RetreatMessage"] = "{0} retreats! Distance: {1}",
                ["SurrenderMessage"] = "{0} abandons!",
                ["WinnerMessage"] = "{0} wins!",
                ["DrawMessage"] = "It's a draw!",
                ["BleedingDamageMessage"] = "{0} takes {1} bleeding damage!",
                ["IsBleedingMessage"] = "{0} is bleeding!",
                ["CannotAttackTitle"] = "Cannot Attack",
                ["CannotAttackMessage"] = "You are too far to attack!",
                ["AlreadyCloseTitle"] = "Already Close",
                ["AlreadyCloseMessage"] = "You cannot advance further!",
                ["OKButton"] = "OK",
                ["TurnLogFormat"] = "Turn {0}: {1}",
                
                // Statistics
                ["StatisticsTitle"] = "Statistics",
                ["TopPlayersTitle"] = "Top Players",
                ["RecentMatchesTitle"] = "Recent Matches",
                ["ResetStatisticsButtonText"] = "Reset Statistics",
                ["ResetStatisticsTitle"] = "Reset Statistics",
                ["ResetStatisticsMessage"] = "Are you sure you want to reset all statistics? This action cannot be undone.",
                ["ResetStatisticsSuccess"] = "Statistics have been reset.",
                
                // How to Play
                ["HowToPlayTitle"] = "How to Play",
                ["HowToPlayMainMenuTitle"] = "Main Menu",
                ["HowToPlayStep1"] = "1. Click 'Start Game' to begin a new match",
                ["HowToPlayStep2"] = "2. View statistics and records",
                ["HowToPlayStep3"] = "3. Access settings and info",
                ["HowToPlayCharSelTitle"] = "Character Selection",
                ["HowToPlayCharSelStep1"] = "1. Enter player names",
                ["HowToPlayCharSelStep2"] = "2. Select race for each player",
                ["HowToPlayCharSelStep3"] = "3. Choose weapons",
                ["HowToPlayCharSelStep4"] = "4. Click 'Play' to start combat",
                ["HowToPlayCombatTitle"] = "Combat",
                ["HowToPlayCombatStep1"] = "1. Use buttons to attack, heal, or move",
                ["HowToPlayCombatStep2"] = "2. Manage distance for ranged attacks",
                ["HowToPlayCombatStep3"] = "3. Defeat opponent to win",
                
                // Technical Info
                ["TechInfoTitle"] = "Technical Information",
                ["TechInfoStackTitle"] = "Technology Stack",
                ["TechInfoStack1"] = ".NET MAUI - Cross-platform framework",
                ["TechInfoStack2"] = "SQLite - Local database",
                ["TechInfoStack3"] = "MVVM Architecture",
                ["TechInfoArchTitle"] = "Architecture",
                ["TechInfoArch1"] = "Model-View-ViewModel pattern for clean separation",
                ["TechInfoDbTitle"] = "Database",
                ["TechInfoDb1"] = "SQLite for player and match history",
                ["TechInfoDb2"] = "Async operations for performance",
                
                // Credits
                ["CreditsTitle"] = "Credits",
                ["CreditsDev"] = "Developed by: Alejandro Bolaños Chinchilla",
                ["CreditsTech"] = "Built with C#, .NET MAUI, SQLite",
                
                // Settings
                ["SettingsText"] = "Settings",
                ["LanguageLabel"] = "Language",
                ["EnglishText"] = "English",
                ["SpanishText"] = "Español",
            },
            ["es"] = new Dictionary<string, string>
            {
                // Main Menu
                ["StartGameText"] = "Iniciar Juego",
                ["ViewStatsText"] = "Estadísticas",
                ["HowToPlayText"] = "Cómo Jugar",
                ["TechnicalInfoText"] = "Información Técnica",
                ["CreditsText"] = "Créditos",
                ["SettingsText"] = "Configuración",
                
                // Character Selection
                ["Player1NameLabel"] = "Nombre Jugador 1",
                ["Player2NameLabel"] = "Nombre Jugador 2",
                ["EnterPlayer1NamePlaceholder"] = "Ingresa nombre del Jugador 1...",
                ["EnterPlayer2NamePlaceholder"] = "Ingresa nombre del Jugador 2...",
                ["SelectWeaponLabel"] = "Seleccionar Arma",
                ["SelectRaceLabel"] = "Seleccionar Raza",
                ["PlayButtonText"] = "Jugar",
                ["RaceElf"] = "Elfo",
                ["RaceHuman"] = "Humano",
                ["RaceDwarf"] = "Enano",
                ["RaceOrc"] = "Orco",
                ["RaceTroll"] = "Trol",
                ["RaceBeast"] = "Bestia",
                ["RaceDwarf"] = "Enano",
                ["RaceTroll"] = "Trol",
                ["HumanName"] = "Humano",
                ["HumanDescription"] = "Raza equilibrada con armas a distancia",
                ["ElfName"] = "Elfo",
                ["ElfDescription"] = "Mago con bastones elementales",
                ["OrcName"] = "Orco",
                ["OrcDescription"] = "Raza fuerte con armas pesadas",
                ["BeastName"] = "Bestia",
                ["BeastDescription"] = "Luchador feroz con habilidades de combate cercano",
                ["DwarfName"] = "Enano",
                ["DwarfDescription"] = "Guerrero resistente con habilidades defensivas",
                ["TrollName"] = "Trol",
                ["TrollDescription"] = "Tanque regenerativo con alta salud",
                ["SwordName"] = "Espada",
                ["AxeName"] = "Hacha",
                ["BowName"] = "Arco",
                ["SniperRifleName"] = "Rifle de Francotirador",
                ["FireStaffName"] = "Bastón de Fuego",
                ["EarthStaffName"] = "Bastón de Tierra",
                ["WaterStaffName"] = "Bastón de Agua",
                ["AirStaffName"] = "Bastón de Aire",
                ["SwordDesc"] = "Arma cuerpo a cuerpo equilibrada",
                ["AxeDesc"] = "Arma pesada, causa sangrado",
                ["BowDesc"] = "Arma a distancia",
                ["SniperRifleDesc"] = "Arma de precisión de largo alcance",
                ["FireStaffDesc"] = "Magia de fuego, ataques a distancia",
                ["EarthStaffDesc"] = "Magia de tierra, defensiva",
                ["WaterStaffDesc"] = "Magia de agua, bonificación de curación",
                ["AirStaffDesc"] = "Magia de aire, ataques rápidos",
                ["ShotgunName"] = "Escopeta",
                ["ShotgunDesc"] = "Arma de fuego poderosa de corto alcance",
                ["HammerName"] = "Martillo",
                ["HammerDesc"] = "Arma pesada que aturde enemigos",
                ["FistsName"] = "Puños",
                ["FistsDesc"] = "Armas naturales, ataques rápidos",
                
                // Button Texts
                ["NextPlayerButtonText"] = "Siguiente Jugador",
                ["StartCombatButtonText"] = "Iniciar Combate",
                
                // Instructions
                ["SelectRaceInstruction"] = "{0}, selecciona tu raza",
                ["SelectWeaponInstruction"] = "{0}, selecciona tu arma",
                
                // Error Messages
                ["ErrorPlayer1Selection"] = "Jugador 1 debe completar su selección",
                ["ErrorPlayer2Selection"] = "Jugador 2 debe completar su selección",
                ["ErrorEnterNames"] = "Por favor ingresa nombres para ambos jugadores",
                ["ErrorDifferentNames"] = "Los jugadores deben tener nombres diferentes",
                ["ErrorCompleteSelection"] = "Por favor completa todas las selecciones",
                ["ErrorTitle"] = "Error",
                ["ErrorMessage"] = "Por favor selecciona razas y armas antes de jugar.",
                
                // Combat
                ["DistanceLabel"] = "Distancia: {0}",
                ["TurnIndicator"] = "Turno: {0}",
                ["HPLabel"] = "HP: {0}",
                ["PlayerLabel"] = "Jugador {0}: {1}",
                ["AttackButtonText"] = "Atacar",
                ["HealButtonText"] = "Curar",
                ["AdvanceButtonText"] = "Avanzar",
                ["RetreatButtonText"] = "Retroceder",
                ["SurrenderButtonText"] = "Abandonar",
                ["ReturnToMenuButtonText"] = "Volver al Menú",
                ["GameOverText"] = "Juego Terminado",
                ["CombatStartedMessage"] = "¡Combate iniciado entre {0} y {1}!",
                ["AttackMessage"] = "¡{0} ataca a {1} por {2} de daño!",
                ["HealMessage"] = "¡{0} se cura {1} HP!",
                ["AdvanceMessage"] = "¡{0} avanza! Distancia: {1}",
                ["RetreatMessage"] = "¡{0} retrocede! Distancia: {1}",
                ["SurrenderMessage"] = "¡{0} se rinde!",
                ["WinnerMessage"] = "¡{0} gana!",
                ["DrawMessage"] = "¡Es un empate!",
                ["BleedingDamageMessage"] = "¡{0} recibe {1} de daño por sangrado!",
                ["IsBleedingMessage"] = "¡{0} está sangrando!",
                ["CannotAttackTitle"] = "No Puede Atacar",
                ["CannotAttackMessage"] = "¡Estás muy lejos para atacar!",
                ["AlreadyCloseTitle"] = "Ya Estás Cerca",
                ["AlreadyCloseMessage"] = "¡No puedes avanzar más!",
                ["OKButton"] = "OK",
                ["YesButton"] = "Sí",
                ["NoButton"] = "No",
                ["SuccessTitle"] = "Éxito",
                ["YesButton"] = "Yes",
                ["NoButton"] = "No",
                ["SuccessTitle"] = "Success",
                ["ErrorTitle"] = "Error",
                ["TurnLogFormat"] = "Turno {0}: {1}",
                
                // Statistics
                ["StatisticsTitle"] = "Estadísticas",
                ["TopPlayersTitle"] = "Mejores Jugadores",
                ["RecentMatchesTitle"] = "Partidas Recientes",
                ["ResetStatisticsButtonText"] = "Reiniciar Estadísticas",
                ["ResetStatisticsTitle"] = "Reiniciar Estadísticas",
                ["ResetStatisticsMessage"] = "¿Estás seguro de que quieres reiniciar todas las estadísticas? Esta acción no se puede deshacer.",
                ["ResetStatisticsSuccess"] = "Las estadísticas han sido reiniciadas.",
                
                // How to Play
                ["HowToPlayTitle"] = "Cómo Jugar",
                ["HowToPlayMainMenuTitle"] = "Menú Principal",
                ["HowToPlayStep1"] = "1. Haz clic en 'Iniciar Juego' para comenzar",
                ["HowToPlayStep2"] = "2. Ver estadísticas y registros",
                ["HowToPlayStep3"] = "3. Accede a configuración e información",
                ["HowToPlayCharSelTitle"] = "Selección de Personajes",
                ["HowToPlayCharSelStep1"] = "1. Ingresa nombres de jugadores",
                ["HowToPlayCharSelStep2"] = "2. Selecciona raza para cada jugador",
                ["HowToPlayCharSelStep3"] = "3. Elige armas",
                ["HowToPlayCharSelStep4"] = "4. Haz clic en 'Jugar' para iniciar combate",
                ["HowToPlayCombatTitle"] = "Combate",
                ["HowToPlayCombatStep1"] = "1. Usa botones para atacar, curar, o moverte",
                ["HowToPlayCombatStep2"] = "2. Gestiona distancia para ataques a distancia",
                ["HowToPlayCombatStep3"] = "3. Derrota al oponente para ganar",
                
                // Technical Info
                ["TechInfoTitle"] = "Información Técnica",
                ["TechInfoStackTitle"] = "Pila Tecnológica",
                ["TechInfoStack1"] = ".NET MAUI - Framework multiplataforma",
                ["TechInfoStack2"] = "SQLite - Base de datos local",
                ["TechInfoStack3"] = "Arquitectura MVVM",
                ["TechInfoArchTitle"] = "Arquitectura",
                ["TechInfoArch1"] = "Patrón Model-View-ViewModel para separación limpia",
                ["TechInfoDbTitle"] = "Base de Datos",
                ["TechInfoDb1"] = "SQLite para jugadores e historial de partidas",
                ["TechInfoDb2"] = "Operaciones asíncronas para rendimiento",
                
                // Credits
                ["CreditsTitle"] = "Créditos",
                ["CreditsDev"] = "Desarrollado por: Alejandro Bolaños Chinchilla",
                ["CreditsTech"] = "Construido con C#, .NET MAUI, SQLite",
                
                // Settings
                ["SettingsText"] = "Configuración",
                ["LanguageLabel"] = "Idioma",
                ["EnglishText"] = "English",
                ["SpanishText"] = "Español",
            }
        };

        private LocalizationManager()
        {
        }

        public void SetLanguage(string languageCode)
        {
            if (_currentLanguage != languageCode && _localizedStrings.ContainsKey(languageCode))
            {
                _currentLanguage = languageCode;
                OnPropertyChanged();
            }
        }

        public string GetString(string key)
        {
            if (_localizedStrings.ContainsKey(_currentLanguage) &&
                _localizedStrings[_currentLanguage].ContainsKey(key))
            {
                return _localizedStrings[_currentLanguage][key];
            }
            return key; // Return key as fallback
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Property accessors - MainMenu
        public string StartGameText => GetString("StartGameText");
        public string ViewStatsText => GetString("ViewStatsText");
        public string HowToPlayText => GetString("HowToPlayText");
        public string TechnicalInfoText => GetString("TechnicalInfoText");
        public string CreditsText => GetString("CreditsText");
        public string SettingsText => GetString("SettingsText");
        
        // Character Selection
        public string Player1NameLabel => GetString("Player1NameLabel");
        public string Player2NameLabel => GetString("Player2NameLabel");
        public string EnterPlayer1NamePlaceholder => GetString("EnterPlayer1NamePlaceholder");
        public string EnterPlayer2NamePlaceholder => GetString("EnterPlayer2NamePlaceholder");
        public string SelectWeaponLabel => GetString("SelectWeaponLabel");
        public string SelectRaceLabel => GetString("SelectRaceLabel");
        public string PlayButtonText => GetString("PlayButtonText");
        public string RaceElf => GetString("RaceElf");
        public string RaceHuman => GetString("RaceHuman");
        public string RaceDwarf => GetString("RaceDwarf");
        public string RaceOrc => GetString("RaceOrc");
        public string RaceTroll => GetString("RaceTroll");
        public string RaceBeast => GetString("RaceBeast");
        public string SwordName => GetString("SwordName");
        public string AxeName => GetString("AxeName");
        public string BowName => GetString("BowName");
        public string SniperRifleName => GetString("SniperRifleName");
        public string FireStaffName => GetString("FireStaffName");
        public string EarthStaffName => GetString("EarthStaffName");
        public string WaterStaffName => GetString("WaterStaffName");
        public string AirStaffName => GetString("AirStaffName");
        public string SwordDesc => GetString("SwordDesc");
        public string AxeDesc => GetString("AxeDesc");
        public string BowDesc => GetString("BowDesc");
        public string SniperRifleDesc => GetString("SniperRifleDesc");
        public string FireStaffDesc => GetString("FireStaffDesc");
        public string EarthStaffDesc => GetString("EarthStaffDesc");
        public string WaterStaffDesc => GetString("WaterStaffDesc");
        public string AirStaffDesc => GetString("AirStaffDesc");
        public string ShotgunName => GetString("ShotgunName");
        public string ShotgunDesc => GetString("ShotgunDesc");
        public string HammerName => GetString("HammerName");
        public string HammerDesc => GetString("HammerDesc");
        public string FistsName => GetString("FistsName");
        public string FistsDesc => GetString("FistsDesc");
        
        // Race Names and Descriptions
        public string HumanName => GetString("HumanName");
        public string HumanDescription => GetString("HumanDescription");
        public string ElfName => GetString("ElfName");
        public string ElfDescription => GetString("ElfDescription");
        public string OrcName => GetString("OrcName");
        public string OrcDescription => GetString("OrcDescription");
        public string BeastName => GetString("BeastName");
        public string BeastDescription => GetString("BeastDescription");
        public string DwarfName => GetString("DwarfName");
        public string DwarfDescription => GetString("DwarfDescription");
        public string TrollName => GetString("TrollName");
        public string TrollDescription => GetString("TrollDescription");
        
        // Button Texts
        public string NextPlayerButtonText => GetString("NextPlayerButtonText");
        public string StartCombatButtonText => GetString("StartCombatButtonText");
        
        // Instructions
        public string SelectRaceInstruction => GetString("SelectRaceInstruction");
        public string SelectWeaponInstruction => GetString("SelectWeaponInstruction");
        
        // Error Messages
        public string ErrorPlayer1Selection => GetString("ErrorPlayer1Selection");
        public string ErrorPlayer2Selection => GetString("ErrorPlayer2Selection");
        public string ErrorEnterNames => GetString("ErrorEnterNames");
        public string ErrorDifferentNames => GetString("ErrorDifferentNames");
        public string ErrorCompleteSelection => GetString("ErrorCompleteSelection");
        public string ErrorTitle => GetString("ErrorTitle");
        public string ErrorMessage => GetString("ErrorMessage");
        
        // Combat
        public string DistanceLabel => GetString("DistanceLabel");
        public string TurnIndicator => GetString("TurnIndicator");
        public string HPLabel => GetString("HPLabel");
        public string PlayerLabel => GetString("PlayerLabel");
        public string AttackButtonText => GetString("AttackButtonText");
        public string HealButtonText => GetString("HealButtonText");
        public string AdvanceButtonText => GetString("AdvanceButtonText");
        public string RetreatButtonText => GetString("RetreatButtonText");
        public string SurrenderButtonText => GetString("SurrenderButtonText");
        public string ReturnToMenuButtonText => GetString("ReturnToMenuButtonText");
        public string GameOverText => GetString("GameOverText");
        public string CombatStartedMessage => GetString("CombatStartedMessage");
        public string AttackMessage => GetString("AttackMessage");
        public string HealMessage => GetString("HealMessage");
        public string AdvanceMessage => GetString("AdvanceMessage");
        public string RetreatMessage => GetString("RetreatMessage");
        public string SurrenderMessage => GetString("SurrenderMessage");
        public string WinnerMessage => GetString("WinnerMessage");
        public string DrawMessage => GetString("DrawMessage");
        public string BleedingDamageMessage => GetString("BleedingDamageMessage");
        public string IsBleedingMessage => GetString("IsBleedingMessage");
        public string CannotAttackTitle => GetString("CannotAttackTitle");
        public string CannotAttackMessage => GetString("CannotAttackMessage");
        public string AlreadyCloseTitle => GetString("AlreadyCloseTitle");
        public string AlreadyCloseMessage => GetString("AlreadyCloseMessage");
        public string OKButton => GetString("OKButton");
        public string YesButton => GetString("YesButton");
        public string NoButton => GetString("NoButton");
        public string SuccessTitle => GetString("SuccessTitle");
        public string TurnLogFormat => GetString("TurnLogFormat");
        
        // Statistics
        public string StatisticsTitle => GetString("StatisticsTitle");
        public string TopPlayersTitle => GetString("TopPlayersTitle");
        public string RecentMatchesTitle => GetString("RecentMatchesTitle");
        public string ResetStatisticsButtonText => GetString("ResetStatisticsButtonText");
        public string ResetStatisticsTitle => GetString("ResetStatisticsTitle");
        public string ResetStatisticsMessage => GetString("ResetStatisticsMessage");
        public string ResetStatisticsSuccess => GetString("ResetStatisticsSuccess");
        
        // How to Play
        public string HowToPlayTitle => GetString("HowToPlayTitle");
        public string HowToPlayMainMenuTitle => GetString("HowToPlayMainMenuTitle");
        public string HowToPlayStep1 => GetString("HowToPlayStep1");
        public string HowToPlayStep2 => GetString("HowToPlayStep2");
        public string HowToPlayStep3 => GetString("HowToPlayStep3");
        public string HowToPlayCharSelTitle => GetString("HowToPlayCharSelTitle");
        public string HowToPlayCharSelStep1 => GetString("HowToPlayCharSelStep1");
        public string HowToPlayCharSelStep2 => GetString("HowToPlayCharSelStep2");
        public string HowToPlayCharSelStep3 => GetString("HowToPlayCharSelStep3");
        public string HowToPlayCharSelStep4 => GetString("HowToPlayCharSelStep4");
        public string HowToPlayCombatTitle => GetString("HowToPlayCombatTitle");
        public string HowToPlayCombatStep1 => GetString("HowToPlayCombatStep1");
        public string HowToPlayCombatStep2 => GetString("HowToPlayCombatStep2");
        public string HowToPlayCombatStep3 => GetString("HowToPlayCombatStep3");
        
        // Technical Info
        public string TechInfoTitle => GetString("TechInfoTitle");
        public string TechInfoStackTitle => GetString("TechInfoStackTitle");
        public string TechInfoStack1 => GetString("TechInfoStack1");
        public string TechInfoStack2 => GetString("TechInfoStack2");
        public string TechInfoStack3 => GetString("TechInfoStack3");
        public string TechInfoArchTitle => GetString("TechInfoArchTitle");
        public string TechInfoArch1 => GetString("TechInfoArch1");
        public string TechInfoDbTitle => GetString("TechInfoDbTitle");
        public string TechInfoDb1 => GetString("TechInfoDb1");
        public string TechInfoDb2 => GetString("TechInfoDb2");
        
        // Credits
        public string CreditsTitle => GetString("CreditsTitle");
        public string CreditsDev => GetString("CreditsDev");
        public string CreditsTech => GetString("CreditsTech");
        
        // Settings
        public string LanguageLabel => GetString("LanguageLabel");
        public string EnglishText => GetString("EnglishText");
        public string SpanishText => GetString("SpanishText");
    }
}
