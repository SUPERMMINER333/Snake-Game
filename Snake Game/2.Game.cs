namespace Snake_Game
{
    public partial class Game : Form
    {

        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        private Circle powerup = new Circle();
        private Circle kill = new Circle();

        int maxWidth;
        int maxHeight;
        int score;
        int highScore;


        string currentPlayerName;

        Random rand = new Random();

        bool goLeft, goRight, goUp, goDown;

        public Game()
        {
            InitializeComponent();

            new Setting();

            // Enable KeyPreview so form receives keyboard events even when controls have focus
            this.KeyPreview = true;

            SetInitialUIState();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
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
            Console.WriteLine($"DEBUG: GameTimer tick - direction={Setting.direction}, goLeft={goLeft}, goRight={goRight}, goUp={goUp}, goDown={goDown}");
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

            //Move the snake
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {

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

                    //Wall Collision
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

                    //Eat food
                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }

                    //Eat powerup
                    if (powerup != null && Snake[i].X == powerup.X && Snake[i].Y == powerup.Y)
                    {
                        EatPowerup();
                    }

                    if (kill != null && Snake[i].X == kill.X && Snake[i].Y == kill.Y)
                    {
                        kill = null;
                        Die();
                    }


                    for (int j = 1; j < Snake.Count; j++)
                    {

                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }
                }
                //Move body
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }

            picCanvas.Invalidate();
        }
        private void UpdateGameBoard(object sender, PaintEventArgs e)
        {
            //Draw snake
            Graphics canvas = e.Graphics;

            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].X * Setting.Width,
                    Snake[i].Y * Setting.Height,
                    Setting.Width, Setting.Height
                    ));
            }

            //Draw food
            canvas.FillEllipse(Brushes.Red, new Rectangle
            (
            food.X * Setting.Width,
            food.Y * Setting.Height,
            Setting.Width, Setting.Height
            ));

            // Draw powerup, nur wenn es existiert
            if (powerup != null)
            {
                canvas.FillEllipse(Brushes.Gold, new Rectangle
                (
                    powerup.X * Setting.Width,
                    powerup.Y * Setting.Height,
                    Setting.Width, Setting.Height
                ));
            }

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
            Console.WriteLine("DEBUG: RestartGame() - Starting");

            // Stop timer if it's running
            gameTime.Stop();

            // Reset all movement flags
            goLeft = false;
            goRight = false;
            goUp = false;
            goDown = false;

            // Reset direction to default
            Setting.direction = "right";
            Setting.Speed = 100;

            SetGameUIState(gameRunning: true);

            maxWidth = picCanvas.Width / Setting.Width - 1;
            maxHeight = picCanvas.Height / Setting.Height - 1;
            Console.WriteLine($"DEBUG: Canvas size: {picCanvas.Width}x{picCanvas.Height}, maxWidth={maxWidth}, maxHeight={maxHeight}");

            // Validate canvas dimensions
            if (maxWidth < 20 || maxHeight < 20)
            {
                throw new InvalidOperationException($"Canvas too small: maxWidth={maxWidth}, maxHeight={maxHeight}. Canvas size: {picCanvas.Width}x{picCanvas.Height}");
            }

            Snake.Clear();

            picCanvas.BackColor = Color.Silver;

            score = 0;
            txtScore.Text = "Score: " + score;

            Circle head = new Circle { X = 15, Y = 15 };
            Snake.Add(head);

            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            Console.WriteLine("DEBUG: About to spawn food");
            SpawnFood();
            Console.WriteLine("DEBUG: About to spawn powerup");
            SpawnPowerup();
            kill = null;

            Console.WriteLine("DEBUG: About to get player highscore");
            GetCurrentPlayerHighscore(currentPlayerName);

            Console.WriteLine($"DEBUG: Setting timer interval to {Setting.Speed} and starting");
            gameTime.Interval = Setting.Speed;
            gameTime.Start();
            Console.WriteLine($"DEBUG: Timer started. Enabled={gameTime.Enabled}");

            // Force canvas redraw
            picCanvas.Invalidate();
            picCanvas.Update();
        }

        private void EatFood()
        {
            score += 1;
            if (Setting.Speed > 10)
                Setting.Speed -= 5;
            else if (Setting.Speed > 1)
                Setting.Speed -= 1;

            txtScore.Text = "Score: " + score;

            Snake.Add(new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            });

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


        private void EatPowerup()
        {
            if (Snake.Count > 2)
            {
                Snake.RemoveAt(Snake.Count - 1);
                Snake.RemoveAt(Snake.Count - 1);
            }

            powerup = null;

            Setting.Speed = 100;
            gameTime.Interval = Setting.Speed;

            // Spawn new powerup with 10% chance
            if (rand.Next(0, 100) < 10)
            {
                SpawnPowerup();
            }

            picCanvas.Invalidate();
        }


        private void Die()
        {
            gameTime.Stop();

            SetGameUIState(gameRunning: false);

            DataBaseUpload(currentPlayerName);

            goDown = false;
            goUp = false;
            goLeft = false;
            goRight = false;

            Setting.Speed = 100;

            picCanvas.BackColor = Color.Red;
            picCanvas.Invalidate();
        }

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

        private void SpawnFood()
        {
            if (maxWidth < 2 || maxHeight < 2)
            {
                Console.WriteLine($"ERROR: Cannot spawn food - invalid dimensions: maxWidth={maxWidth}, maxHeight={maxHeight}");
                throw new InvalidOperationException($"Cannot spawn food - invalid dimensions: maxWidth={maxWidth}, maxHeight={maxHeight}");
            }

            do
            {
                food = new Circle
                {
                    X = rand.Next(2, maxWidth),
                    Y = rand.Next(2, maxHeight)
                };
            } while (IsPositionOccupied(food.X, food.Y));
        }

        private void SpawnPowerup()
        {
            if (maxWidth < 2 || maxHeight < 2)
            {
                Console.WriteLine($"ERROR: Cannot spawn powerup - invalid dimensions: maxWidth={maxWidth}, maxHeight={maxHeight}");
                powerup = null;
                return;
            }

            Circle newPowerup;
            int maxDistance = -1;
            Circle head = Snake[0];

            for (int attempt = 0; attempt < 100; attempt++)
            {
                int x = rand.Next(2, maxWidth);
                int y = rand.Next(2, maxHeight);
                newPowerup = new Circle { X = x, Y = y };

                if (IsPositionOccupied(x, y)) continue;

                int distance = Math.Abs(head.X - newPowerup.X) +
                               Math.Abs(head.Y - newPowerup.Y);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    powerup = newPowerup;
                }
            }
        }

        private void SpawnKillPowerup()
        {
            Circle newKill;
            int maxDistance = -1;
            Circle head = Snake[0];

            for (int attempt = 0; attempt < 100; attempt++)
            {
                int x = rand.Next(2, maxWidth);
                int y = rand.Next(2, maxHeight);
                newKill = new Circle { X = x, Y = y };

                if (IsPositionOccupied(x, y)) continue;

                int distance = Math.Abs(head.X - newKill.X) + Math.Abs(head.Y - newKill.Y);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    kill = newKill;
                }
            }
        }

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
                PlayerNameText.Visible = false;
                PlayerNameText.Enabled = false;
                PlayerNameTextBox.Visible = false;
                PlayerNameTextBox.Enabled = false;
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
                Console.WriteLine("DEBUG: Enter key pressed");
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

                currentPlayerName = playerName;
                Console.WriteLine($"DEBUG: Player name set to: {currentPlayerName}");

                PlayerNameTextBox.Enabled = false;

                // Prevent the beep sound and consume the Enter key event
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Transfer focus to the form so keyboard input works for game controls
                this.Focus();

                Console.WriteLine("DEBUG: About to call RestartGame()");
                try
                {
                    RestartGame();
                    Console.WriteLine("DEBUG: RestartGame() completed successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"DEBUG: RestartGame() failed with error: {ex.Message}");
                    Console.WriteLine($"DEBUG: Stack trace: {ex.StackTrace}");
                    MessageBox.Show($"Failed to start game: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void GetCurrentPlayerHighscore(string currentPlayername)
        {
            try
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
            catch (Exception ex)
            {
                // If database connection fails, set default values and continue game startup
                highScore = 0;
                txtHighScore.Text = "High Score: -";
                txtHighScore.ForeColor = Color.Black;

                // Log the error to console for debugging
                Console.WriteLine($"Database error while fetching high score: {ex.Message}");
            }
        }
    }
}
