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

        int count = 0;

        int playerName;

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
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void GameTimer(object sender, EventArgs e)
        {
            // setting the directions

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
            // end of directions

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

                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }


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

            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head); // adding the head part of the snake to the list

            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            gameTime.Start();
        }

        private void EatFood()
        {
            score += 1;

            txtScore.Text = "Score: " + score;

            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(body);

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
        }

        private void Die()
        {
            gameTime.Stop();

            if (score > highScore)
            {
                highScore = score;

                txtHighScore.Text = "High Score: " + highScore;
                txtHighScore.ForeColor = Color.Maroon;
            }

            StopButton.Enabled = true;
            StopButton.Visible = true;
            RefreshButton.Enabled = true;
            RefreshButton.Visible = true;

            DataBaseUpload(PlayerNameTextBox.Text);
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

        public void DataBaseUpload(string playerName)
        {

            try
            {
                using var db = new Models.SnakeGameContext();

                var newName = new Models.SnakeGame
                {
                    PlayerName = playerName,
                    Score = score,
                    Level = Setting.Level,
                    Speed = Setting.Speed
                };

                db.SnakeGames.Add(newName);
                db.SaveChanges();

                MessageBox.Show("Your score has been submitted successfully!", "Submission Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            catch
            {
                MessageBox.Show("An error occurred while submitting your score. Please try again later.", "Submission Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                
                PlayerNameTextBox.Enabled = false;
                RestartGame();
            }
        }
    }
}
