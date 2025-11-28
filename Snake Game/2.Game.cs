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
        // Snake body parts
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        private Circle powerup = new Circle();
        private Circle kill = new Circle();
        // Game area dimensions
        int maxWidth;
        int maxHeight;
        int score;
        int highScore;

        // Current player name
        string currentPlayerName;

        // Random number generator
        Random rand = new Random();

        // Movement flags
        bool goLeft, goRight, goUp, goDown;

        public Game()
        {
            // Initialize the Game components
            InitializeComponent();

            // Initialize settings
            new Setting();
            
            // UI
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
            BackButton.Visible = false;
            BackButton.Enabled = false;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Set movement flags on key press
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (Setting.direction != "right")
                {
                    goLeft = true;
                    goRight = false;
                    goUp = false;
                    goDown = false;
                }
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (Setting.direction != "left")
                {
                    goRight = true;
                    goLeft = false;
                    goUp = false;
                    goDown = false;
                }
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if (Setting.direction != "down")
                {
                    goUp = true;
                    goLeft = false;
                    goRight = false;
                    goDown = false;
                }
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                if (Setting.direction != "up")
                {
                    goDown = true;
                    goLeft = false;
                    goRight = false;
                    goUp = false;
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                Game newGame = new Game();
                newGame.Show();
            }
            if (e.KeyCode == Keys.F1)
            {
                if (gameTime.Enabled == true)
                {
                    Application.Exit();
                }
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            // Reset movement flags on key release
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

        private void GameTimer(object sender, EventArgs e)
        {
            // Direction
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

            // Move the snake
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    // Move head
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

                    // Wall Collision
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

                    // Eat food
                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }

                    // Eat powerup
                    if (powerup != null && Snake[i].X == powerup.X && Snake[i].Y == powerup.Y)
                    {
                        EatPowerup();
                    }

                    // Eat kill powerup
                    if (kill != null && Snake[i].X == kill.X && Snake[i].Y == kill.Y)
                    {
                        kill = null;
                        Die();
                    }

                    // Self Collision
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }
                }
                // Move body
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
            // Redraw the game board
            picCanvas.Invalidate();
        }
        private void UpdateGameBoard(object sender, PaintEventArgs e)
        {
            // Draw snake
            Graphics canvas = e.Graphics;
            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                // Draw head
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                // Draw snake
                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].X * Setting.Width,
                    Snake[i].Y * Setting.Height,
                    Setting.Width, Setting.Height
                    ));
            }

            // Draw food
            canvas.FillEllipse(Brushes.Red, new Rectangle
            (
            food.X * Setting.Width,
            food.Y * Setting.Height,
            Setting.Width, Setting.Height
            ));

            // Draw powerup, if it exists
            if (powerup != null)
            {
                canvas.FillEllipse(Brushes.Gold, new Rectangle
                (
                    powerup.X * Setting.Width,
                    powerup.Y * Setting.Height,
                    Setting.Width, Setting.Height
                ));
            }

            // Draw kill powerup, nur wenn es existiert
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

        private void RestartGame()
        {
            // UI
            StopButton.Enabled = false;
            StopButton.Visible = false;
            RefreshButton.Enabled = false;
            RefreshButton.Visible = false;
            PlayerNameText.Visible = false;
            PlayerNameText.Enabled = false;
            PlayerNameTextBox.Visible = false;
            PlayerNameTextBox.Enabled = false;
            RipLabel.Visible = false;
            RipLabel.Enabled = false;
            BackButton.Visible = false;
            BackButton.Enabled = false;

            // Set max width and height
            maxWidth = picCanvas.Width / Setting.Width - 1;
            maxHeight = picCanvas.Height / Setting.Height - 1;

            // Reset snake
            Snake.Clear();

            // Change background color
            picCanvas.BackColor = Color.Silver;

            // Reset score
            score = 0;
            txtScore.Text = "Score: " + score;

            // Create head
            Circle head = new Circle { X = 15, Y = 15 };
            Snake.Add(head);

            // Add initial body parts
            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            // Spawn food
            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            powerup = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            kill = null;

            // Get current player's high score
            GetCurrentPlayerHighscore(currentPlayerName);
            
            // Start the game
            gameTime.Interval = Setting.Speed;
            gameTime.Start();
        }

        private void EatFood()
        {
            // Increase score
            score += 1;
            if (Setting.Speed > 10)
                Setting.Speed -= 5;
            else if (Setting.Speed > 1)
                Setting.Speed -= 1;

            // Update game speed
            gameTime.Interval = Setting.Speed;

            // Update score
            txtScore.Text = "Score: " + score;

            // Add circle to snake
            Snake.Add(new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            });

            // Spawn food in a position that doesn't overlap with snake or other items
            for (int attempt = 0; attempt < 100; attempt++)
            {
                // Generate random position
                int x = rand.Next(2, maxWidth);
                int y = rand.Next(2, maxHeight);
                Circle newFood = new Circle { X = x, Y = y };
                bool isOccupied = false;

                // Check if position overlaps with snake
                foreach (var segment in Snake)
                {
                    if (segment.X == newFood.X && segment.Y == newFood.Y)
                    {
                        isOccupied = true;
                        break;
                    }
                }

                // Check if position overlaps with powerup
                if (!isOccupied && powerup != null && powerup.X == newFood.X && powerup.Y == newFood.Y)
                {
                    isOccupied = true;
                }

                // Check if position overlaps with kill
                if (!isOccupied && kill != null && kill.X == newFood.X && kill.Y == newFood.Y)
                {
                    isOccupied = true;
                }

                // If position is free, place the food there
                if (!isOccupied)
                {
                    food = newFood;
                    break;
                }
            }

            // Despawn existing powerups when eating food
            powerup = null;
            kill = null;

            // Spawn new powerup with 15% chance
            if (rand.Next(0, 100) < 15)
            {
                // Spawn new powerup
                Circle newPowerup;
                int maxDistance = -1;
                Circle head = Snake[0];

                for (int attempt = 0; attempt < 100; attempt++)
                {
                    // Generate random position
                    int x = rand.Next(2, maxWidth);
                    int y = rand.Next(2, maxHeight);
                    newPowerup = new Circle { X = x, Y = y };
                    bool isOccupied = false;

                    // Check if position overlaps with snake
                    foreach (var segment in Snake)
                    {
                        if (segment.X == newPowerup.X && segment.Y == newPowerup.Y)
                        {
                            isOccupied = true;
                            break;
                        }
                    }

                    // Check if position overlaps with food
                    if (!isOccupied && food.X == newPowerup.X && food.Y == newPowerup.Y)
                    {
                        isOccupied = true;
                    }

                    // Check if position overlaps with kill
                    if (!isOccupied && kill != null && kill.X == newPowerup.X && kill.Y == newPowerup.Y)
                    {
                        isOccupied = true;
                    }

                    // Check if position overlaps with existing powerup
                    if (isOccupied) continue;

                    // Find the position that is farthest from the snake's head
                    int distance = Math.Abs(head.X - newPowerup.X) +
                                   Math.Abs(head.Y - newPowerup.Y);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        powerup = newPowerup;
                    }
                }
                // Redraw canvas to show new powerup
                picCanvas.Invalidate();
            }

            // Spawn new kill powerup with 10% chance
            if (rand.Next(0, 100) < 10)
            {
                Circle newKill;
                int maxDistance = -1;
                Circle head = Snake[0];

                for (int attempt = 0; attempt < 100; attempt++)
                {
                    // Generate random position
                    int x = rand.Next(2, maxWidth);
                    int y = rand.Next(2, maxHeight);
                    newKill = new Circle { X = x, Y = y };
                    bool isOccupied = false;

                    // Check if position overlaps with snake
                    foreach (var segment in Snake)
                    {
                        if (segment.X == newKill.X && segment.Y == newKill.Y)
                        {
                            isOccupied = true;
                            break;
                        }
                    }

                    // Check if position overlaps with food
                    if (!isOccupied && food.X == newKill.X && food.Y == newKill.Y)
                    {
                        isOccupied = true;
                    }

                    // Check if position overlaps with powerup
                    if (!isOccupied && powerup != null && powerup.X == newKill.X && powerup.Y == newKill.Y)
                    {
                        isOccupied = true;
                    }
                    // Check if position overlaps with existing kill
                    if (isOccupied) continue;

                    // Find the position that is farthest from the snake's head
                    int distance = Math.Abs(head.X - newKill.X) + Math.Abs(head.Y - newKill.Y);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        kill = newKill;
                    }
                }
                // Redraw canvas to show new kill powerup
                picCanvas.Invalidate();
            }
            // Debug
            Console.WriteLine(rand.Next(0, 100).ToString());
        }


        private void EatPowerup()
        {
            // Decrease score
            if (Snake.Count > 2)
            {
                Snake.RemoveAt(Snake.Count - 1);
                Snake.RemoveAt(Snake.Count - 1);
            }

            // Despawn powerup
            powerup = null;

            // Increase speed
            Setting.Speed = 100;
            gameTime.Interval = Setting.Speed;

            if (rand.Next(0, 100) < 10)
            {
                // Spawn new kill powerup
                Circle newPowerup;
                int maxDistance = -1;
                Circle head = Snake[0];

                // Spawn new powerup
                for (int attempt = 0; attempt < 100; attempt++)
                {
                    int x = rand.Next(2, maxWidth);
                    int y = rand.Next(2, maxHeight);
                    newPowerup = new Circle { X = x, Y = y };

                    bool isOccupied = false;

                    // Check if position overlaps with snake
                    foreach (var segment in Snake)
                    {
                        if (segment.X == newPowerup.X && segment.Y == newPowerup.Y)
                        {
                            isOccupied = true;
                            break;
                        }
                    }

                    // Check if position overlaps with food
                    if (!isOccupied && food.X == newPowerup.X && food.Y == newPowerup.Y)
                    {
                        isOccupied = true;
                    }

                    // Check if position overlaps with kill
                    if (!isOccupied && kill != null && kill.X == newPowerup.X && kill.Y == newPowerup.Y)
                    {
                        isOccupied = true;
                    }

                    // Check if position overlaps with existing powerup
                    if (isOccupied) continue;

                    int distance = Math.Abs(head.X - newPowerup.X) +
                                   Math.Abs(head.Y - newPowerup.Y);

                    // Find the position that is farthest from the snake's head
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        powerup = newPowerup;
                    }
                }
            }
            picCanvas.Invalidate();
        }


        private void Die()
        {
            // Stop the game
            gameTime.Stop();

            // UI
            StopButton.Enabled = true;
            StopButton.Visible = true;
            RefreshButton.Enabled = true;
            RefreshButton.Visible = true;
            RipLabel.Visible = true;
            RipLabel.Enabled = true;
            BackButton.Visible = true;
            BackButton.Enabled = true;

            // Upload score to database
            DataBaseUpload(currentPlayerName);

            // Reset variables
            goDown = false;
            goUp = false;
            goLeft = false;
            goRight = false;
            Setting.Speed = 100;

            // Change background color
            picCanvas.BackColor = Color.LightCoral;
            picCanvas.Invalidate();
        }

        private void StopGame(object sender, EventArgs e)
        {
            // Exit application
            Application.Exit();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            // Restart the game
            StopButton.Enabled = false;
            StopButton.Visible = false;
            RefreshButton.Enabled = false;
            RefreshButton.Visible = false;
            PlayerNameTextBox.Enabled = false;
            PlayerNameTextBox.Visible = false;
            PlayerNameText.Enabled = false;
            PlayerNameText.Visible = false;
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

                // UI
                PlayerNameTextBox.Clear();
                StopButton.Enabled = true;
                StopButton.Visible = true;
                RefreshButton.Enabled = true;
                RefreshButton.Visible = true;
                PlayerNameTextBox.Enabled = false;
                PlayerNameTextBox.Visible = false;
                PlayerNameText.Enabled = false;
                PlayerNameText.Visible = false;
                txtHighScore.ForeColor = Color.Black;
            }
            // Error Message
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
            // Save player name on Enter key press
            if (e.KeyCode == Keys.Enter)
            {
                var playerName = PlayerNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    MessageBox.Show("Please enter a valid name before submitting your score.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (playerName.Length > 20)
                {
                    MessageBox.Show("Player name is too long. Please enter a name with 20 characters or fewer.", "Name Too Long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save the current player name
                currentPlayerName = playerName;

                // Disable the TextBox to prevent further changes
                PlayerNameTextBox.Enabled = false;
                RestartGame();
            }
        }

        public void GetCurrentPlayerHighscore(string currentPlayername)
        {
            // Retrieve high score from database
            using var db = new Models.SnakeGameContext();

            // Normalize the player name for case-insensitive comparison
            var nameLower = currentPlayerName.ToLower().Trim();

            // Query the database for the player's high score
            var playerHighScore = db.SnakeGames.FirstOrDefault(x => x.PlayerName.ToLower() == nameLower);

            // Update the high score display
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

        private void BackButton_Click(object sender, EventArgs e)
        {
            // Navigate back to Main Menu
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }
    }
}
