using Snake_Game.Models;

namespace Snake_Game
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SnakeGameContext snakeGameContext = new SnakeGameContext())
                {
                    snakeGameContext.Database.CanConnect();
                    StartButton.Enabled = false;
                    StopButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message);
            }

            Game Game = new Game();
            Game.Show();
            this.Hide();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StatsButton_Click(object sender, EventArgs e)
        {
            RankingList RankingList = new RankingList();
            RankingList.Show();
            this.Hide();
        }
    }
}
