using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GAME.Models;

namespace GAME.ViewModels
{
    public class CharacterSelectionViewModel : BaseViewModel
    {
        private string _player1Name;
        private string _player2Name;
        private RaceType? _player1SelectedRace;
        private RaceType? _player2SelectedRace;
        private WeaponType? _player1SelectedWeapon;
        private WeaponType? _player2SelectedWeapon;
        private int _currentPlayer = 1;
        private string _instructionText;
        private ObservableCollection<WeaponInfo> _availableWeapons = new();
        private ObservableCollection<RaceInfo> _races;
        private RaceInfo _selectedRaceInfo;
        private WeaponInfo _selectedWeapon;

        public string Player1Name
        {
            get => _player1Name;
            set => SetProperty(ref _player1Name, value);
        }

        public string Player2Name
        {
            get => _player2Name;
            set => SetProperty(ref _player2Name, value);
        }

        public RaceType? Player1SelectedRace
        {
            get => _player1SelectedRace;
            set => SetProperty(ref _player1SelectedRace, value);
        }

        public RaceType? Player2SelectedRace
        {
            get => _player2SelectedRace;
            set => SetProperty(ref _player2SelectedRace, value);
        }

        public WeaponType? Player1SelectedWeapon
        {
            get => _player1SelectedWeapon;
            set => SetProperty(ref _player1SelectedWeapon, value);
        }

        public WeaponType? Player2SelectedWeapon
        {
            get => _player2SelectedWeapon;
            set => SetProperty(ref _player2SelectedWeapon, value);
        }

        public int CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                SetProperty(ref _currentPlayer, value);
                OnPropertyChanged(nameof(IsPlayer1Turn));
                OnPropertyChanged(nameof(IsPlayer2Turn));
                OnPropertyChanged(nameof(NextButtonText));
            }
        }

        public bool IsPlayer1Turn => CurrentPlayer == 1;
        public bool IsPlayer2Turn => CurrentPlayer == 2;
        public string NextButtonText => CurrentPlayer == 1 
            ? Services.LocalizationManager.Instance.NextPlayerButtonText 
            : Services.LocalizationManager.Instance.StartCombatButtonText;

        public string InstructionText
        {
            get => _instructionText;
            set => SetProperty(ref _instructionText, value);
        }

        public ObservableCollection<WeaponInfo> AvailableWeapons
        {
            get => _availableWeapons;
            set => SetProperty(ref _availableWeapons, value);
        }

        public ObservableCollection<RaceInfo> Races
        {
            get => _races;
            set => SetProperty(ref _races, value);
        }

        public WeaponInfo SelectedWeapon
        {
            get => _selectedWeapon;
            set
            {
                if (SetProperty(ref _selectedWeapon, value) && value != null)
                {
                    SelectWeapon(value);
                }
            }
        }

        public RaceInfo SelectedRaceInfo
        {
            get => _selectedRaceInfo;
            set
            {
                SetProperty(ref _selectedRaceInfo, value);
                if (value != null)
                {
                    SelectRace(value.Race);
                }
            }
        }

        public ICommand NextCommand { get; }
        public ICommand SelectRaceCommand { get; }
        public ICommand SelectWeaponCommand { get; }

        public CharacterSelectionViewModel()
        {
            NextCommand = new Command(async () => await Next());
            SelectRaceCommand = new Command<RaceInfo>(race => SelectedRaceInfo = race);
            SelectWeaponCommand = new Command<WeaponInfo>(weapon => SelectedWeapon = weapon);

            Services.LocalizationManager.Instance.PropertyChanged += (s, e) =>
            {
                UpdateLocalizedData();
                OnPropertyChanged(nameof(NextButtonText));
                UpdateInstructions();
            };

            UpdateLocalizedData();
            UpdateInstructions();
        }

        private void UpdateLocalizedData()
        {
            var loc = Services.LocalizationManager.Instance;
            
            // Preserve selection if possible, but for now just reload list
            var currentRace = SelectedRaceInfo?.Race;
            
            Races = new ObservableCollection<RaceInfo>
            {
                new RaceInfo { Race = RaceType.Human, Name = loc.HumanName, Image = "human.jpg", Description = loc.HumanDescription },
                new RaceInfo { Race = RaceType.Elf, Name = loc.ElfName, Image = "elf.jpg", Description = loc.ElfDescription },
                new RaceInfo { Race = RaceType.Orc, Name = loc.OrcName, Image = "orc.jpg", Description = loc.OrcDescription },
                new RaceInfo { Race = RaceType.Beast, Name = loc.BeastName, Image = "best.jpg", Description = loc.BeastDescription }
            };

            if (currentRace.HasValue)
            {
                SelectedRaceInfo = Races.FirstOrDefault(r => r.Race == currentRace.Value);
            }

            UpdateAvailableWeapons();
        }

        private void SelectRace(RaceType race)
        {
            if (CurrentPlayer == 1)
            {
                Player1SelectedRace = race;
            }
            else
            {
                Player2SelectedRace = race;
            }
            SelectedWeapon = null;
            UpdateInstructions();
            UpdateAvailableWeapons();
        }

        private void SelectWeapon(WeaponInfo weaponInfo)
        {
            if (weaponInfo == null)
                return;

            if (CurrentPlayer == 1)
            {
                Player1SelectedWeapon = weaponInfo.Type;
            }
            else
            {
                Player2SelectedWeapon = weaponInfo.Type;
            }
            UpdateInstructions();
        }

        private void UpdateInstructions()
        {
            var playerName = CurrentPlayer == 1 ? Player1Name : Player2Name;
            if (string.IsNullOrWhiteSpace(playerName))
                playerName = CurrentPlayer == 1 ? Services.LocalizationManager.Instance.PlayerLabel.Replace("{0}", "1").Replace(": {1}", "") : Services.LocalizationManager.Instance.PlayerLabel.Replace("{0}", "2").Replace(": {1}", "");
            
            // Fallback if name is empty
            if (playerName.Contains("{")) playerName = CurrentPlayer == 1 ? "Player 1" : "Player 2";

            if ((CurrentPlayer == 1 && Player1SelectedRace == null) || (CurrentPlayer == 2 && Player2SelectedRace == null))
            {
                InstructionText = string.Format(Services.LocalizationManager.Instance.SelectRaceInstruction, playerName);
            }
            else
            {
                InstructionText = string.Format(Services.LocalizationManager.Instance.SelectWeaponInstruction, playerName);
            }
        }

        private async Task Next()
        {
            var loc = Services.LocalizationManager.Instance;
            if (CurrentPlayer == 1)
            {
                if (string.IsNullOrWhiteSpace(Player1Name) || Player1SelectedRace == null || Player1SelectedWeapon == null)
                {
                    await Application.Current.MainPage.DisplayAlert(loc.ErrorTitle, loc.ErrorPlayer1Selection, "OK");
                    return;
                }
                CurrentPlayer = 2;
                UpdateInstructions();
                SelectedRaceInfo = null;
                SelectedWeapon = null;
                AvailableWeapons = new ObservableCollection<WeaponInfo>();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Player2Name) || Player2SelectedRace == null || Player2SelectedWeapon == null)
                {
                    await Application.Current.MainPage.DisplayAlert(loc.ErrorTitle, loc.ErrorPlayer2Selection, "OK");
                    return;
                }
                await StartCombat();
            }
        }

        private async Task StartCombat()
        {
            var loc = Services.LocalizationManager.Instance;
            if (string.IsNullOrWhiteSpace(Player1Name) || string.IsNullOrWhiteSpace(Player2Name))
            {
                await Application.Current.MainPage.DisplayAlert(loc.ErrorTitle, loc.ErrorEnterNames, "OK");
                return;
            }

            if (Player1Name == Player2Name)
            {
                await Application.Current.MainPage.DisplayAlert(loc.ErrorTitle, loc.ErrorDifferentNames, "OK");
                return;
            }

            if (Player1SelectedRace == null || Player1SelectedWeapon == null ||
                Player2SelectedRace == null || Player2SelectedWeapon == null)
            {
                await Application.Current.MainPage.DisplayAlert(loc.ErrorTitle, loc.ErrorCompleteSelection, "OK");
                return;
            }

            // Navigate to combat
            await Shell.Current.GoToAsync($"combat?player1={Player1Name}&player2={Player2Name}&p1race={Player1SelectedRace}&p1weapon={Player1SelectedWeapon}&p2race={Player2SelectedRace}&p2weapon={Player2SelectedWeapon}");
        }

        public List<RaceType> GetAvailableRaces()
        {
            return new List<RaceType> { RaceType.Human, RaceType.Elf, RaceType.Orc, RaceType.Beast };
        }

        public List<WeaponType> GetAvailableWeapons(RaceType race)
        {
            return race switch
            {
                RaceType.Human => new List<WeaponType> { WeaponType.Shotgun, WeaponType.SniperRifle },
                RaceType.Elf => new List<WeaponType> { WeaponType.FireStaff, WeaponType.EarthStaff, WeaponType.AirStaff, WeaponType.WaterStaff },
                RaceType.Orc => new List<WeaponType> { WeaponType.Axe, WeaponType.Hammer },
                RaceType.Beast => new List<WeaponType> { WeaponType.Fists, WeaponType.Sword },
                _ => new List<WeaponType>()
            };
        }
    
        private void UpdateAvailableWeapons()
        {
            RaceType? selectedRace = CurrentPlayer == 1 ? Player1SelectedRace : Player2SelectedRace;
            if (!selectedRace.HasValue)
            {
                AvailableWeapons = new ObservableCollection<WeaponInfo>();
                return;
            }

            var weapons = GetAvailableWeapons(selectedRace.Value)
                .Select(CreateWeaponInfo);

            AvailableWeapons = new ObservableCollection<WeaponInfo>(weapons);
        }

        private WeaponInfo CreateWeaponInfo(WeaponType weapon)
        {
            var loc = Services.LocalizationManager.Instance;
            var (name, image, description) = weapon switch
            {
                WeaponType.Shotgun => (loc.ShotgunName, "bow.png", loc.ShotgunDesc),
                WeaponType.SniperRifle => (loc.SniperRifleName, "longsword.png", loc.SniperRifleDesc),
                WeaponType.FireStaff => (loc.FireStaffName, "trident.png", loc.FireStaffDesc),
                WeaponType.EarthStaff => (loc.EarthStaffName, "hammer.png", loc.EarthStaffDesc),
                WeaponType.AirStaff => (loc.AirStaffName, "mini_sword.png", loc.AirStaffDesc),
                WeaponType.WaterStaff => (loc.WaterStaffName, "sword.png", loc.WaterStaffDesc),
                WeaponType.Axe => (loc.AxeName, "exe.png", loc.AxeDesc),
                WeaponType.Hammer => (loc.HammerName, "hammer.png", loc.HammerDesc),
                WeaponType.Fists => (loc.FistsName, "gauntlet.png", loc.FistsDesc),
                WeaponType.Sword => (loc.SwordName, "trident.png", loc.SwordDesc),
                _ => (weapon.ToString(), "dotnet_bot.png", "A trusty tool for adventuring.")
            };

            return new WeaponInfo
            {
                Type = weapon,
                Name = name,
                Image = image,
                Description = description
            };
        }
        public void Reset()
        {
            Player1Name = string.Empty;
            Player2Name = string.Empty;
            Player1SelectedRace = null;
            Player2SelectedRace = null;
            Player1SelectedWeapon = null;
            Player2SelectedWeapon = null;
            CurrentPlayer = 1;
            SelectedRaceInfo = null;
            SelectedWeapon = null;
            UpdateInstructions();
            UpdateAvailableWeapons();
        }
    }
}
