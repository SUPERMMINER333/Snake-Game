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

        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

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

            StopButton.Enabled = true;
            StopButton.Visible = true;
            RefreshButton.Enabled = false;
            RefreshButton.Visible = false;
            PlayerNameText.Visible = true;
            PlayerNameText.Enabled = true;
            PlayerNameTextBox.Visible = true;
            PlayerNameTextBox.Enabled = true;
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
            canvas.FillEllipse(Brushes.DarkRed, new Rectangle
            (
            food.X * Setting.Width,
            food.Y * Setting.Height,
            Setting.Width, Setting.Height
            ));

        }
        
        private void RestartGame()
        {
            StopButton.Enabled = false;
            StopButton.Visible = false;
            RefreshButton.Enabled = false;
            RefreshButton.Visible = false;
            PlayerNameText.Visible = false;
            PlayerNameText.Enabled = false;
            PlayerNameTextBox.Visible = false;
            PlayerNameTextBox.Enabled = false;

            maxWidth = picCanvas.Width / Setting.Width - 1;
            maxHeight = picCanvas.Height / Setting.Height - 1;

            Snake.Clear();


            score = 0;
            txtScore.Text = "Score: " + score;

            Circle head = new Circle { X = 15, Y = 15 };
            Snake.Add(head);

            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            GetCurrentPlayerHighscore(currentPlayerName);

            gameTime.Interval = Setting.Speed;
            gameTime.Start();
        }

        private void EatFood()
        {
            score += 1;
            if (Setting.Speed > 10)
            {
                Setting.Speed -= 5;
            }
            else
            {
                if (Setting.Speed > 1)
                {
                    Setting.Speed -= 1;
                }
            }

            txtScore.Text = "Score: " + score;

            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(body);

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            gameTime.Interval = Setting.Speed;
        }

        private void Die()
        {
            gameTime.Stop();

            StopButton.Enabled = true;
            StopButton.Visible = true;
            RefreshButton.Enabled = true;
            RefreshButton.Visible = true;

            DataBaseUpload(currentPlayerName);

            goDown = false;
            goUp = false;
            goLeft = false;
            goRight = false;

            Setting.Speed = 100;
        }

        private void StopGame(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
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
                        PlayerExists.Speed = Setting.Speed;
                    }

                }
                else
                {
                    var Submit = new Models.SnakeGame
                    {
                        PlayerName = currentPlayerName,
                        Score = score,
                        Speed = Setting.Speed
                    };
                    db.SnakeGames.Add(Submit);
                }

                db.SaveChanges();

                //UI
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
                    return;
                }
                if (playerName.Length > 20)
                {
                    MessageBox.Show("Player name is too long. Please enter a name with 20 characters or fewer.", "Name Too Long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                currentPlayerName = playerName;
                
                PlayerNameTextBox.Enabled = false;
                RestartGame();
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
                MessageBox.Show("-*");
            }

        }
    }
}
