using Snake_Game.Models;

namespace Snake_Game
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            // Initialize the main menu components
            InitializeComponent();

            // Check database connection
            using var db = new Models.SnakeGameContext();
            if (!db.Database.CanConnect())
            {
                MessageBox.Show("No connection to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            // Start a new game
            Game Game = new Game();
            Game.Show();
            this.Hide();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            // Exit the application
            Application.Exit();
        }

        private void StatsButton_Click(object sender, EventArgs e)
        {
            // Show the ranking list
            RankingList RankingList = new RankingList();
            RankingList.Show();
            this.Hide();
        }
    }
}
