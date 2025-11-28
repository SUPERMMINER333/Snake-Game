using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_Game
{
    internal class Setting
    {
        // Static properties to hold game settings
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static string? direction { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }

        public Setting()
        {
            // Initialize default settings
            Width = 16;
            Height = 16;
            direction = "right";
            Speed = 100;
            Score = 0;
        }
    }
}
