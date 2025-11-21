using System;
using System.Collections.Generic;

namespace Snake_Game.Models;

public partial class SnakeGame
{
    public int Id { get; set; }

    public string? PlayerName { get; set; }

    public int? Speed { get; set; }

    public int? Score { get; set; }
}
