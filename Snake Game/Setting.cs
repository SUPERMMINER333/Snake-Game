using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_Game
{
    internal class Setting
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static string? direction { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Level { get; set; }
        public static string? Difficulty { get; set; }

        public Setting()
        {
            Width = 16;
            Height = 16;
            direction = "right";
            Speed = 16;
            Score = 0;
        }
    }
}
