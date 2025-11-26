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

        public Setting()
        {
            Width = 16;
            Height = 16;
            direction = "right";
            Speed = 100;
            Score = 0;
        }
    }
}
