## 🐍 Snake Game v1.0.1

A modern implementation of the classic Snake game built with .NET 10.0 Windows Forms, featuring persistent high scores and competitive gameplay.

---

## ✨ Features

### 🎮 Core Gameplay
- Classic snake mechanics with smooth controls
- Dynamic food spawning system
- Power-ups for enhanced gameplay
- Obstacles and challenges to increase difficulty
- Real-time score tracking

### 🏆 Competitive Features
- **Global Ranking System** - Compete with other players for the top spot if you are connected to the same database
- **Persistent High Scores** - Scores are saved to a MySQL database
- **Player Profiles** - Enter your name and track your progress
- **Leaderboard** - View top players and their achievements

### 💻 Technical Highlights
- Built with **.NET 10.0** and **Windows Forms**
- **Entity Framework Core 9.0** for data management
- **MySQL** database integration via Pomelo provider
- Clean, maintainable codebase with German documentation

---

## 🚀 Quick Start

### 1️⃣ Download & Extract
Download the release assets below and extract to your desired location.

### 2️⃣ Setup Database
Start your MySQL/MariaDB server and run:

```sql
CREATE DATABASE snake_game;
USE snake_game;

CREATE TABLE snake_game (
  ID int(11) NOT NULL AUTO_INCREMENT,
  PlayerName varchar(50) DEFAULT NULL,
  Score int(11) DEFAULT NULL,
  PRIMARY KEY (ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
```

### 3️⃣ Configure Connection (Optional)
**Default:** `server=localhost;database=snake_game;user=root;password=`

To change: Edit `Snake Game/Models/SnakeGameContext.cs` line 23.

### 4️⃣ Play!
Run `Snake Game.exe` and enjoy! 🎮

---

## 🎮 How to Play

| Control | Action |
|---------|--------|
| `↑` `↓` `←` `→` | Move snake |
| Enter name | Track your score |
| Collect food | Grow & score points |
| Avoid obstacles | Stay alive! |

---

## 📋 System Requirements

- **OS:** Windows 10/11
- **.NET Runtime:** .NET 10.0 or higher ([Download](https://dotnet.microsoft.com/download/dotnet/10.0))
- **Database:** MySQL 8.0+ or MariaDB 10.4+ ([XAMPP](https://www.apachefriends.org/) recommended)

---

## 🆕 What's New

- ✅ Initial public release
- ✅ Complete gameplay with smooth controls
- ✅ MySQL database integration for high scores
- ✅ Global leaderboard system
- ✅ Player profile management
- ✅ Power-ups and obstacles

---

## 📖 Documentation

Full documentation available in [README.md](../../blob/main/README.md)

---

## 🐛 Found a Bug?

Report issues [here](../../issues) or contribute via [pull request](../../pulls)

---

**Enjoy the game and happy hunting! 🎯**

⭐ **Star this repo if you like it!** ⭐

*Made with ❤️ and .NET*
