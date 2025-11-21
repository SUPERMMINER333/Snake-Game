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
            Game Game = new Game();
            Game.Show();
            this.Hide();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
