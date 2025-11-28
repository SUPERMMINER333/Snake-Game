# 🐍 Snake Game v1.0.1

A modern implementation of the classic Snake game built with .NET 10.0 Windows Forms, featuring persistent high scores and competitive gameplay.

---

## ✨ Features

### 🎮 Core Gameplay
- **Classic snake mechanics** with smooth controls
- **Dynamic food spawning** system
- **Power-ups** for enhanced gameplay
- **Obstacles and challenges** to increase difficulty
- **Real-time score tracking**

### 🏆 Competitive Features
- **Global Ranking System** - Compete with other players for the top spot if you are connected to the same database
- **💾 Persistent High Scores** - Scores are saved to a MySQL database
- **👤 Player Profiles** - Enter your name and track your progress
- **📊 Leaderboard** - View top players and their achievements

### 💻 Technical Highlights
- Built with **.NET 10.0** and **Windows Forms**
- **Entity Framework Core 9.0** for data management
- **MySQL** database integration via Pomelo provider
- Clean, maintainable codebase with German documentation

---

## 🎮 How to Play

1. **Launch** the game and enter your player name
2. **Use arrow keys** to control the snake
3. **Collect food** to grow and increase your score
4. **Avoid obstacles** and don't hit yourself!
5. **Try to beat** the high score and climb the leaderboard

---

## 📋 System Requirements

| Requirement | Version |
|-------------|---------|
| **OS** | Windows 10/11 |
| **.NET Runtime** | .NET 10.0 or higher |
| **Database** | MySQL Server 8.0+ or MariaDB 10.4+ |

---

## 🚀 Quick Start Installation

### 1. Download the Release
- Download the latest release from the assets below
- Extract the archive to your desired location

### 2. Setup MySQL Database

Start your MySQL/MariaDB server and run:

```sql
CREATE DATABASE snake_game;
USE snake_game;

CREATE TABLE `snake_game` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PlayerName` varchar(50) DEFAULT NULL,
  `Score` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
```

### 3. Configure Connection (if needed)

**Default settings:**
- Server: `localhost`
- Database: `snake_game`
- User: `root`
- Password: *(empty)*

**To change the connection:**

Open `Snake Game/Models/SnakeGameContext.cs` and modify line 23:

```csharp
"server=your_server;database=snake_game;user=your_user;password=your_password"
```

> ⚠️ **Security Note**: For production use, move the connection string to a configuration file or use environment variables.

### 4. Run the Game
Double-click `Snake Game.exe` and enjoy! 🎉

---

## 📊 Database Setup Details

The SQL schema is also available in `Snake Game/Models/DataBaseRest.txt` for your convenience.

**Default Configuration:**
- **Server**: `localhost`
- **Database**: `snake_game`
- **User**: `root`
- **Password**: *(empty)*

---

## 🆕 What's New in v1.0.1

- ✅ Initial public release
- ✅ Complete gameplay implementation
- ✅ MySQL database integration
- ✅ Global leaderboard system
- ✅ Player profile management
- ✅ Smooth controls and game mechanics

---

## 🐛 Known Issues

No known issues at this time. Please report bugs in the [Issues](../../issues) section.

---

## 📖 Full Documentation

For detailed documentation, building from source, and contributing guidelines, see the [README.md](../../blob/main/README.md).

---

## 🙏 Acknowledgments

Thank you for playing Snake Game! If you enjoy the game, please:
- ⭐ Star this repository
- 🐛 Report bugs or suggest features
- 🤝 Contribute to the project

---

**Enjoy the game and happy hunting! 🎯**

*Made with ❤️ and .NET*
