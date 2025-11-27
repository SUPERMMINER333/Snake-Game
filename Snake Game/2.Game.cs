using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Game : Form
    {
        // Game objects
        private List<Circle> Snake = new List<Circle>();  // Snake body segments
        private Circle food = new Circle();                // Regular food item (red)
        private Circle powerup = new Circle();             // Speed powerup (gold) - makes snake shorter and slower
        private Circle kill = new Circle();                // Kill powerup (dark red) - instant death

        // Game boundaries
        int maxWidth;   // Maximum grid width
        int maxHeight;  // Maximum grid height

        // Score tracking
        int score;      // Current score
        int highScore;  // Player's high score

        // Player info
        string currentPlayerName;

        // Random number generator for spawning items
        Random rand = new Random();

        // Movement flags for keyboard input
        bool goLeft, goRight, goUp, goDown;

        /// <summary>
        /// Initializes a new game instance
        /// </summary>
        public Game()
        {
            InitializeComponent();

            new Setting();

            SetInitialUIState();
        }

        /// <summary>
        /// Handles keyboard key press events for controlling snake direction
        /// Supports arrow keys and WASD controls
        /// Prevents 180-degree turns (e.g., can't go right if currently going left)
        /// </summary>
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Left movement (Left Arrow or A key)
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (Setting.direction != "right")  // Prevent reversing into itself
                {
                    goLeft = true;
                    goRight = false;
                    goUp = false;
                    goDown = false;
                }
            }
            // Right movement (Right Arrow or D key)
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (Setting.direction != "left")  // Prevent reversing into itself
                {
                    goRight = true;
                    goLeft = false;
                    goUp = false;
                    goDown = false;
                }
            }
            // Up movement (Up Arrow or W key)
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if (Setting.direction != "down")  // Prevent reversing into itself
                {
                    goUp = true;
                    goLeft = false;
                    goRight = false;
                    goDown = false;
                }
            }
            // Down movement (Down Arrow or S key)
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                if (Setting.direction != "up")  // Prevent reversing into itself
                {
                    goDown = true;
                    goLeft = false;
                    goRight = false;
                    goUp = false;
                }
            }
            // Escape key - restart game
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                Game newGame = new Game();
                newGame.Show();
            }
            // F1 key - exit application (only during active game)
            if (e.KeyCode == Keys.F1)
            {
                if (gameTime.Enabled == true)
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Handles keyboard key release events
        /// Resets movement flags when keys are released
        /// </summary>
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                goDown = false;
            }
        }

        /// <summary>
        /// Main game loop timer - handles snake movement, collision detection, and item collection
        /// Called repeatedly at intervals determined by game speed
        /// </summary>
        private void GameTimer(object sender, EventArgs e)
        {
            // Update direction based on current movement flags
            if (goLeft)
            {
                Setting.direction = "left";
            }
            if (goRight)
            {
                Setting.direction = "right";
            }
            if (goDown)
            {
                Setting.direction = "down";
            }
            if (goUp)
            {
                Setting.direction = "up";
            }

            // Move the snake - process from tail to head
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)  // Head of snake
                {
                    // Move head in current direction
                    switch (Setting.direction)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                    }

                    // Check wall collisions (game boundaries)
                    if (Snake[i].X < 0)
                    {
                        Die();
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Die();
                    }
                    if (Snake[i].Y < 0)
                    {
                        Die();
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Die();
                    }

                    // Check food collection
                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }

                    // Check powerup collection (gold item - makes snake shorter and slower)
                    if (powerup != null && Snake[i].X == powerup.X && Snake[i].Y == powerup.Y)
                    {
                        EatPowerup();
                    }

                    // Check kill powerup collision (dark red item - instant death)
                    if (kill != null && Snake[i].X == kill.X && Snake[i].Y == kill.Y)
                    {
                        kill = null;
                        Die();
                    }

                    // Check self-collision (snake hitting its own body)
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }
                }
                // Move body segments - each segment follows the one in front
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }

            picCanvas.Invalidate();
        }

        /// <summary>
        /// Renders all game objects on the canvas
        /// Draws the snake, food, and powerups (if they exist)
        /// </summary>
        private void UpdateGameBoard(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Brush snakeColour;

            // Draw snake - head is black, body is dark green
            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;  // Head
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;  // Body
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].X * Setting.Width,
                    Snake[i].Y * Setting.Height,
                    Setting.Width, Setting.Height
                    ));
            }

            // Draw food (red)
            canvas.FillEllipse(Brushes.Red, new Rectangle
            (
            food.X * Setting.Width,
            food.Y * Setting.Height,
            Setting.Width, Setting.Height
            ));

            // Draw gold powerup (only if it exists)
            if (powerup != null)
            {
                canvas.FillEllipse(Brushes.Gold, new Rectangle
                (
                    powerup.X * Setting.Width,
                    powerup.Y * Setting.Height,
                    Setting.Width, Setting.Height
                ));
            }

            // Draw kill powerup (dark red, only if it exists)
            if (kill != null)
            {
                canvas.FillEllipse(Brushes.DarkRed, new Rectangle
                (
                    kill.X * Setting.Width,
                    kill.Y * Setting.Height,
                    Setting.Width, Setting.Height
                ));
            }

        }

        /// <summary>
        /// Resets the game to initial state and starts a new game
        /// Clears the board, spawns initial items, and starts the game timer
        /// </summary>
        private void RestartGame()
        {
            SetGameUIState(gameRunning: true);

            // Calculate game grid dimensions
            maxWidth = picCanvas.Width / Setting.Width - 1;
            maxHeight = picCanvas.Height / Setting.Height - 1;

            // Clear existing snake
            Snake.Clear();

            // Reset canvas color
            picCanvas.BackColor = Color.Silver;

            // Reset score
            score = 0;
            txtScore.Text = "Score: " + score;

            // Create initial snake (head + 10 body segments)
            Circle head = new Circle { X = 15, Y = 15 };
            Snake.Add(head);

            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            // Spawn initial items
            SpawnFood();
            SpawnPowerup();
            kill = null;  // No kill powerup at start

            // Load player's high score
            GetCurrentPlayerHighscore(currentPlayerName);

            // Start game timer
            gameTime.Interval = Setting.Speed;
            gameTime.Start();
        }

        /// <summary>
        /// Handles food collection - increases score, grows snake, speeds up game
        /// Has chance to spawn powerups (15% for gold, 10% for kill)
        /// </summary>
        private void EatFood()
        {
            // Increase score
            score += 1;

            // Gradually increase game speed (decrease interval)
            if (Setting.Speed > 10)
                Setting.Speed -= 5;
            else if (Setting.Speed > 1)
                Setting.Speed -= 1;

            txtScore.Text = "Score: " + score;

            // Grow snake by adding new segment at tail position
            Snake.Add(new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            });

            // Spawn new food at a free position
            SpawnFood();

            // Spawn new powerup with 15% chance (only if none exists)
            if (powerup == null && rand.Next(0, 100) < 15)
            {
                SpawnPowerup();
            }

            // Spawn new kill powerup with 10% chance (only if none exists)
            if (kill == null && rand.Next(0, 100) < 10)
            {
                SpawnKillPowerup();
            }

            // Update game speed and refresh canvas
            gameTime.Interval = Setting.Speed;
            picCanvas.Invalidate();
            Console.WriteLine(rand.Next(0, 100).ToString());
        }

        /// <summary>
        /// Handles gold powerup collection - shrinks snake and slows down game
        /// Has 10% chance to spawn another powerup
        /// </summary>
        private void EatPowerup()
        {
            // Shrink snake by removing 2 tail segments (if snake is long enough)
            if (Snake.Count > 2)
            {
                Snake.RemoveAt(Snake.Count - 1);
                Snake.RemoveAt(Snake.Count - 1);
            }

            // Remove current powerup
            powerup = null;

            // Slow down game to base speed
            Setting.Speed = 100;
            gameTime.Interval = Setting.Speed;

            // Spawn new powerup with 10% chance
            if (rand.Next(0, 100) < 10)
            {
                SpawnPowerup();
            }

            picCanvas.Invalidate();
        }

        /// <summary>
        /// Handles game over - stops game, updates UI, saves score to database
        /// </summary>
        private void Die()
        {
            // Stop game timer
            gameTime.Stop();

            // Update UI to show game over state
            SetGameUIState(gameRunning: false);

            // Save score to database
            DataBaseUpload(currentPlayerName);

            // Reset movement flags
            goDown = false;
            goUp = false;
            goLeft = false;
            goRight = false;

            // Reset speed to base value
            Setting.Speed = 100;

            // Change canvas to red to indicate death
            picCanvas.BackColor = Color.Red;
            picCanvas.Invalidate();
        }

        /// <summary>
        /// Checks if a grid position is occupied by snake, food, or any powerup
        /// Used to prevent items from spawning on top of each other
        /// </summary>
        /// <param name="x">X coordinate on grid</param>
        /// <param name="y">Y coordinate on grid</param>
        /// <returns>True if position is occupied, false otherwise</returns>
        private bool IsPositionOccupied(int x, int y)
        {
            // Check if position is on snake
            foreach (var segment in Snake)
            {
                if (segment.X == x && segment.Y == y)
                    return true;
            }

            // Check if position is on food
            if (food != null && food.X == x && food.Y == y)
                return true;

            // Check if position is on powerup
            if (powerup != null && powerup.X == x && powerup.Y == y)
                return true;

            // Check if position is on kill powerup
            if (kill != null && kill.X == x && kill.Y == y)
                return true;

            return false;
        }

        /// <summary>
        /// Spawns food at a random free position on the grid
        /// Keeps generating positions until a free one is found
        /// </summary>
        private void SpawnFood()
        {
            do
            {
                food = new Circle
                {
                    X = rand.Next(2, maxWidth),
                    Y = rand.Next(2, maxHeight)
                };
            } while (IsPositionOccupied(food.X, food.Y));
        }

        /// <summary>
        /// Spawns gold powerup as far as possible from snake head
        /// Tries 100 positions and picks the one furthest from the head
        /// Skips occupied positions
        /// </summary>
        private void SpawnPowerup()
        {
            Circle newPowerup;
            int maxDistance = -1;
            Circle head = Snake[0];

            // Try 100 random positions and pick the furthest from snake head
            for (int attempt = 0; attempt < 100; attempt++)
            {
                int x = rand.Next(2, maxWidth);
                int y = rand.Next(2, maxHeight);
                newPowerup = new Circle { X = x, Y = y };

                if (IsPositionOccupied(x, y)) continue;

                // Calculate Manhattan distance from head
                int distance = Math.Abs(head.X - newPowerup.X) +
                               Math.Abs(head.Y - newPowerup.Y);

                // Keep track of furthest position
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    powerup = newPowerup;
                }
            }
        }

        /// <summary>
        /// Spawns kill powerup as far as possible from snake head
        /// Tries 100 positions and picks the one furthest from the head
        /// Skips occupied positions
        /// </summary>
        private void SpawnKillPowerup()
        {
            Circle newKill;
            int maxDistance = -1;
            Circle head = Snake[0];

            // Try 100 random positions and pick the furthest from snake head
            for (int attempt = 0; attempt < 100; attempt++)
            {
                int x = rand.Next(2, maxWidth);
                int y = rand.Next(2, maxHeight);
                newKill = new Circle { X = x, Y = y };

                if (IsPositionOccupied(x, y)) continue;

                // Calculate Manhattan distance from head
                int distance = Math.Abs(head.X - newKill.X) + Math.Abs(head.Y - newKill.Y);

                // Keep track of furthest position
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    kill = newKill;
                }
            }
        }

        /// <summary>
        /// Sets UI to initial state - waiting for player name entry
        /// </summary>
        private void SetInitialUIState()
        {
            // Initial state - waiting for player name
            StopButton.Enabled = true;
            StopButton.Visible = true;
            RefreshButton.Enabled = false;
            RefreshButton.Visible = false;
            PlayerNameText.Visible = true;
            PlayerNameText.Enabled = true;
            PlayerNameTextBox.Visible = true;
            PlayerNameTextBox.Enabled = true;
            RipLabel.Visible = false;
            RipLabel.Enabled = false;
        }

        private void SetGameUIState(bool gameRunning)
        {
            if (gameRunning)
            {
                // Game is running - hide all UI except stop button
                StopButton.Enabled = true;
                StopButton.Visible = true;
                RefreshButton.Enabled = false;
                RefreshButton.Visible = false;
                PlayerNameText.Visible = false;
                PlayerNameText.Enabled = false;
                PlayerNameTextBox.Visible = false;
                PlayerNameTextBox.Enabled = false;
                RipLabel.Visible = false;
                RipLabel.Enabled = false;
            }
            else
            {
                // Game is over - show restart options
                StopButton.Enabled = true;
                StopButton.Visible = true;
                RefreshButton.Enabled = true;
                RefreshButton.Visible = true;
                RipLabel.Visible = true;
                RipLabel.Enabled = true;
            }
        }

        private void StopGame(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RestartGame();
            txtHighScore.ForeColor = Color.Black;
        }

        public void DataBaseUpload(string currentPlayerName)
        {
            try
            {
                using var db = new Models.SnakeGameContext();
                var PlayerExists = db.SnakeGames.FirstOrDefault(x => x.PlayerName.ToLower() == currentPlayerName.ToLower());

                if (PlayerExists != null)
                {
                    if (score > PlayerExists.Score)
                    {
                        PlayerExists.Score = score;
                    }

                }
                else
                {
                    var Submit = new Models.SnakeGame
                    {
                        PlayerName = currentPlayerName,
                        Score = score,
                    };
                    db.SnakeGames.Add(Submit);
                }

                db.SaveChanges();

                // Clear player name textbox after successful submission
                PlayerNameTextBox.Clear();
                txtHighScore.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while submitting your score. Please try again later.", "Submission Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(
                    $"ERROR:\n{ex.Message}\n\nINNER:\n{ex.InnerException?.Message}\n\nSTACKTRACE:\n{ex.StackTrace}",
                    "Submission Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void SavaPlayerName(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var playerName = PlayerNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    MessageBox.Show("Please enter a valid name before submitting your score.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Handled = true;
                    return;
                }
                if (playerName.Length > 20)
                {
                    MessageBox.Show("Player name is too long. Please enter a name with 20 characters or fewer.", "Name Too Long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Handled = true;
                    return;
                }

                currentPlayerName = playerName;

                // Prevent the Enter key from causing issues
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Hide and disable the textbox before starting the game
                PlayerNameTextBox.Enabled = false;
                PlayerNameTextBox.Visible = false;

                // Start the game
                RestartGame();

                // Set focus to the game canvas
                picCanvas.Focus();
            }
        }

        public void GetCurrentPlayerHighscore(string currentPlayername)
        {
            using var db = new Models.SnakeGameContext();

            var nameLower = currentPlayerName.ToLower().Trim();

            var playerHighScore = db.SnakeGames.FirstOrDefault(x => x.PlayerName.ToLower() == nameLower);

            if (playerHighScore != null)
            {
                highScore = (int)playerHighScore.Score;

                txtHighScore.Text = "High Score: " + highScore;
                txtHighScore.ForeColor = Color.Maroon;
            }
            else
            {
                highScore = 0;

                txtHighScore.Text = "High Score: -";
            }

        }
    }
}
