using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Snake_Game
{
    public partial class RankingList : Form
    {
        public RankingList()
        {
            // Initialize the Ranking list components
            InitializeComponent();

            // Adjust columns when the form is resized
            this.Resize += (s, e) => AdjustColumnWidths();
            LoadRankingList();
            AdjustColumnWidths();
        }

        // Load ranking data from the database
        private async Task LoadRankingList()
        {
            // Clear existing items
            ListViewRanking.Items.Clear();

            // Initialize database context
            using var db = new Models.SnakeGameContext();
            if (!db.Database.CanConnect())
            {
                var li = new ListViewItem(new[] { "-", "No connection to the database.", "-" });
                ListViewRanking.Items.Add(li);
                AdjustColumnWidths();
                return;
            }

            // Retrieve top 100 scores ordered by score descending
            var rankings = await db.SnakeGames
                             .OrderByDescending(s => s.Score)
                             .ToListAsync();

            // Initialize rank counter
            int rank = 1;

            foreach (var game in rankings)
            {
                // Safeguard against null or empty player names and scores
                var player = string.IsNullOrWhiteSpace(game.PlayerName) ? "Unknown" : game.PlayerName;
                var scoreText = (game.Score.HasValue) ? game.Score.Value.ToString() : "0";

                // Create ListView item
                var item = new ListViewItem(rank.ToString());
                item.SubItems.Add(player);
                item.SubItems.Add(scoreText);

                // Add item to ListView
                ListViewRanking.Items.Add(item);
                rank++;
            }

            // Adjust column widths after loading data
            AdjustColumnWidths();
        }

        private void AdjustColumnWidths()
        {
            // Ensure ListViewRanking is not null
            if (ListViewRanking == null) return;

            int clientWidth = ListViewRanking.ClientSize.Width;
            int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;

            // Get current column widths
            int rankWidth = Math.Max(80, colRank.Width);
            int scoreWidth = Math.Max(80, colScore.Width);
            int nameWidth = clientWidth - rankWidth - scoreWidth - 4;

            // Adjust for scrollbar if needed
            if (ListViewRanking.Items.Count > ListViewRanking.ClientSize.Height / (int)Math.Max(1, ListViewRanking.Font.Height))
            {
                nameWidth -= scrollbarWidth;
            }

            // Ensure name column has a minimum width
            if (nameWidth < 50) nameWidth = 50;

            // Set new column widths
            colRank.Width = rankWidth;
            colName.Width = nameWidth;
            colScore.Width = scoreWidth;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            // Navigate back to the main menu
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }
    }
}
