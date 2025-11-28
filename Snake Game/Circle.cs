using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_Game
{
    internal class Circle
    {
        // Properties for X and Y coordinates
        public int X { get; set; }
        public int Y { get; set; }

        public Circle()
        {
            // Initialize coordinates to zero
            X = 0;
            Y = 0;
        }
    }
}
