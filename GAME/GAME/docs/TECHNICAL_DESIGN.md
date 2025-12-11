# Technical Design

## Game Rules

### Initial Health
- Default: all players start with 100 HP.
- Exception: Water Elf starts with 115 HP.

### Races, Weapons, Abilities & Healing

#### Humans
- Can only use firearms:
  - Shotgun
  - Sniper Rifle
- **Shotgun**:
  - Base damage: random integer in range [1–5].
  - Adds a small extra percentage bonus (10% of rolled damage).
- **Sniper Rifle (at normal distance)**:
  - Base damage: random integer in range [1–5].
- **Healing**:
  - Humans can spend a turn to “Eat”.
  - Healing restores 45% of the missing HP.

#### Elves
- Use magic only, channeled through a staff.
- Must choose a magic element:
  - Fire
  - Earth
  - Air
  - Water
- Element effects:
  - **Fire**: +25% damage bonus.
  - **Earth**: +10% attack bonus.
  - **Air**: normal base damage.
  - **Water**: increases starting HP and improves healing.
- **Healing**:
  - Normal Elves: heal 65% of missing HP.
  - Water Elves: heal 80% of missing HP.

#### Orcs
- Choose between:
  - Axe
  - Hammer
- **Axe**:
  - Base damage: random [1–5].
  - Applies bleeding for 2 extra turns, target loses −3 HP at the start of each of the next 2 turns.
- **Hammer**:
  - Base damage: random [2–7].
- **Healing**:
  - Orcs spend a turn to drink a healing potion.
  - Healing has two components:
    - First immediately: 35% of missing HP.
    - Second on the next turn: extra 15% of missing HP.

#### Beasts
- Attacks:
  - **Fists**:
    - High fixed physical damage: random between 20–30 HP to the opponent.
    - Self-damage: the Beast loses 10 HP each time it uses Fists.
  - **Sword**:
    - Random damage between 1–10 HP.
- **Healing**:
  - Beasts spend a turn to “Sleep”.
  - Healing restores exactly 50% of the HP lost so far.

### Turn System, Distance & Movement
- The game is strictly turn-based.
- Distance is represented by a numeric value (0 = melee).
- Players can use their turn to: Move Forward, Move Backward, Attack, Heal.
- Melee attacks are only allowed when distance == 0.
- Ranged attacks at distance:
  - **Sniper Rifle**: damage range increases to 10–20 HP.
  - **Air Magic**: damage is increased by an extra +10%.

## Architecture

```
JuegoPRU/
├── Models/
│   ├── Player.cs
│   ├── Character.cs
│   ├── GameState.cs
│   └── MatchHistory.cs
├── ViewModels/
│   ├── BaseViewModel.cs
│   ├── MainViewModel.cs
│   ├── CharacterSelectionViewModel.cs
│   ├── CombatViewModel.cs
│   └── StatisticsViewModel.cs
├── Views/
│   ├── MainPage.xaml
│   ├── CharacterSelectionPage.xaml
│   ├── CombatPage.xaml
│   ├── StatisticsPage.xaml
│   ├── HowToPlayPage.xaml
│   ├── CreditsPage.xaml
│   ├── RulesPage.xaml
│   └── TechnicalInfoPage.xaml
├── Services/
│   └── DatabaseService.cs
└── Resources/
    └── Images/
        ├── human_shotgun.png
        ├── human_sniperrifle.png
        ├── elf_fire.png
        ├── elf_earth.png
        ├── elf_air.png
        ├── elf_water.png
        ├── orc_axe.png
        ├── orc_hammer.png
        ├── beast_fists.png
        └── beast_sword.png
```

## Database Schema

### Players
- `Id` (INTEGER, PRIMARY KEY)
- `Name` (TEXT, UNIQUE)
- `TotalWins` (INTEGER)
- `TotalLosses` (INTEGER)
- `TotalDraws` (INTEGER)
- `GamesPlayed` (INTEGER)

### MatchHistory
- `Id` (INTEGER, PRIMARY KEY)
- `Player1Name` (TEXT)
- `Player2Name` (TEXT)
- `WinnerName` (TEXT)
- `ResultType` (TEXT)
- `Player1FinalHealth` (INTEGER)
- `Player2FinalHealth` (INTEGER)
- `TurnsPlayed` (INTEGER)
- `PlayedAt` (DATETIME)