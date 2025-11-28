# 🐍 Snake Game

A modern, feature-rich implementation of the classic Snake game built with .NET 10.0 Windows Forms. Compete globally with persistent high scores stored in a MySQL database!

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/Windows%20Forms-blue?logo=windows)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
[![MySQL](https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=white)](https://www.mysql.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

## 📋 Table of Contents

- [Features](#-features)
- [Screenshots](#-screenshots)
- [Prerequisites](#-prerequisites)
- [Installation](#-installation)
- [Database Setup](#-database-setup)
- [How to Play](#-how-to-play)
- [Building from Source](#-building-from-source)
- [Configuration](#-configuration)
- [Project Structure](#-project-structure)
- [Technologies Used](#-technologies-used)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features

### 🎮 Gameplay
- **Classic Snake Mechanics** - Navigate the snake to collect food and grow
- **Multiple Game Elements**:
  - 🍎 Food items for points and growth
  - ⚡ Power-ups for bonus effects
  - ☠️ Obstacles to avoid
- **Smooth Controls** - Responsive arrow key navigation
- **Real-time Score Tracking** - Watch your score climb as you play

### 🏆 Competitive Features
- **Global Leaderboard** - Compete with players worldwide
- **Persistent High Scores** - All scores saved to MySQL database
- **Player Profiles** - Create and track your personal best
- **Ranking System** - View top players and their achievements

### 💻 Technical Features
- Built with **.NET 10.0** (latest framework)
- **Entity Framework Core 9.0** for robust data access
- **MySQL/MariaDB** integration via Pomelo provider
- Clean, maintainable architecture
- Comprehensive German code documentation

## 🔧 Prerequisites

Before you begin, ensure you have the following installed:

- **Operating System**: Windows 10 or Windows 11
- **.NET SDK**: [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- **Database**: MySQL Server 8.0+ or MariaDB 10.4+
  - Download [MySQL](https://dev.mysql.com/downloads/mysql/)
  - Or [XAMPP](https://www.apachefriends.org/) (includes MariaDB)

## 🚀 Installation

### Option 1: Download Pre-built Release (Recommended)

1. Go to the [Releases](../../releases) page
2. Download the latest version
3. Extract the ZIP file to your desired location
4. Set up the database (see [Database Setup](#-database-setup))
5. Run `Snake Game.exe`

### Option 2: Build from Source

See [Building from Source](#-building-from-source) section below.

## 🗄️ Database Setup

### Step 1: Create Database

Start your MySQL/MariaDB server and create the database:

```sql
CREATE DATABASE snake_game;
USE snake_game;
```

### Step 2: Create Table

Run the following SQL script (also available in `Snake Game/Models/DataBaseRest.txt`):

```sql
CREATE TABLE `snake_game` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PlayerName` varchar(50) DEFAULT NULL,
  `Score` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
```

### Step 3: Configure Connection String

**Default Configuration:**
- **Server**: `localhost`
- **Database**: `snake_game`
- **User**: `root`
- **Password**: *(empty)*

**To change the connection string:**

1. Open `Snake Game/Models/SnakeGameContext.cs`
2. Locate line 23:
   ```csharp
   => optionsBuilder.UseMySql("server=localhost;database=snake_game;user=root", ...);
   ```
3. Modify the connection string as needed:
   ```csharp
   "server=your_server;database=snake_game;user=your_user;password=your_password"
   ```

> **⚠️ Security Note**: For production use, move the connection string to a configuration file or use environment variables.

## 🎮 How to Play

### Starting the Game

1. Launch `Snake Game.exe`
2. Enter your player name in the main menu
3. Click **Start** to begin

### Controls

| Key | Action |
|-----|--------|
| `↑` | Move Up |
| `↓` | Move Down |
| `←` | Move Left |
| `→` | Move Right |
| `Stop` | Pause/Stop Game |
| `Refresh` | Restart Game |

### Game Rules

1. **Objective**: Eat food to grow your snake and increase your score
2. **Growth**: Each food item makes your snake longer
3. **Power-ups**: Collect special items for bonuses
4. **Avoid**: Don't hit obstacles or yourself!
5. **Scoring**: The longer you survive and more you eat, the higher your score

### Viewing Leaderboard

- Click **Ranking List** from the main menu
- See the top players and their scores
- Your best score will appear in the global rankings!

## 🔨 Building from Source

### Clone the Repository

```bash
git clone https://github.com/SUPERMMINER333/Snake-Game.git
cd Snake-Game
```

### Build with .NET CLI

```bash
# Restore dependencies
dotnet restore "Snake Game/Snake Game.csproj"

# Build the project
dotnet build "Snake Game/Snake Game.csproj" --configuration Release

# Run the application
dotnet run --project "Snake Game/Snake Game.csproj"
```

### Build with Visual Studio

1. Open `Snake Game.slnx` in Visual Studio 2022 or later
2. Ensure you have the **.NET desktop development** workload installed
3. Press `F5` to build and run
4. Or use `Ctrl+Shift+B` to build only

### Using GitHub Actions

This project includes a CI/CD workflow that automatically:
- Builds the project on every push
- Runs tests (if available)
- Creates artifacts for download
- Can deploy releases with code signing

See `.github/workflows/dotnet-build.yml` for details.

## ⚙️ Configuration

### Adjusting Game Settings

Game settings can be modified in `Snake Game/Setting.cs`:

```csharp
public class Setting
{
    // Customize game speed, difficulty, colors, etc.
}
```

### Database Configuration

- **Context**: `Snake Game/Models/SnakeGameContext.cs`
- **Entity Model**: `Snake Game/Models/SnakeGame.cs`
- **SQL Schema**: `Snake Game/Models/DataBaseRest.txt`

## 📁 Project Structure

```
Snake-Game/
├── .github/
│   └── workflows/
│       └── dotnet-build.yml      # CI/CD workflow
├── Snake Game/
│   ├── Models/
│   │   ├── SnakeGame.cs          # Entity model
│   │   ├── SnakeGameContext.cs   # EF Core DbContext
│   │   └── DataBaseRest.txt      # SQL schema
│   ├── Properties/               # Application properties
│   ├── Resources/                # Game resources
│   ├── 1.MainMenu.cs            # Main menu form
│   ├── 2.Game.cs                # Game logic
│   ├── 3.RankingList.cs         # Leaderboard
│   ├── Circle.cs                # Snake segment/item class
│   ├── Setting.cs               # Game settings
│   ├── Program.cs               # Entry point
│   └── Snake Game.csproj        # Project file
├── .gitignore
├── .gitattributes
├── Snake Game.slnx              # Solution file
└── README.md                    # This file
```

## 🛠️ Technologies Used

| Technology | Version | Purpose |
|------------|---------|---------|
| [.NET](https://dotnet.microsoft.com/) | 10.0 | Application framework |
| [Windows Forms](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/) | - | Desktop UI |
| [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) | 9.0.0 | ORM for database access |
| [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql) | 9.0.0 | MySQL provider for EF Core |
| [MySQL](https://www.mysql.com/) / [MariaDB](https://mariadb.org/) | 10.4+ | Database for high scores |

## 📧 Contact

**SUPERMMINER333** - [@SUPERMMINER333](https://github.com/SUPERMMINER333)

Project Link: [https://github.com/SUPERMMINER333/Snake-Game](https://github.com/SUPERMMINER333/Snake-Game)

---

</div>
