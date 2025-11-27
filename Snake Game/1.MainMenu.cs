namespace Snake_Game
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();

            using var db = new Models.SnakeGameContext();
            if (!db.Database.CanConnect())
            {
                MessageBox.Show("No connection to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
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
