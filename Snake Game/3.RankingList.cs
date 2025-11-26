using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Snake_Game
{
    public partial class RankingList : Form
    {
        public RankingList()
        {
            InitializeComponent();
            // Adjust columns when the form is resized
            this.Resize += (s, e) => AdjustColumnWidths();
            LoadRankingList();
            AdjustColumnWidths();
        }

        // Load ranking data from the database
        private void LoadRankingList()
        {
            ListViewRanking.Items.Clear();

            try
            {
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
                var rankings = db.SnakeGames
                                 .OrderByDescending(s => s.Score)
                                 .Take(100)
                                 .ToList();

                // Handle case with no entries
                if (rankings.Count == 0)
                {
                    var li = new ListViewItem(new[] { "-", "No entries in the database.", "-" });
                    ListViewRanking.Items.Add(li);
                    AdjustColumnWidths();
                    return;
                }

                int rank = 1;

                // Populate the ListView with ranking data
                foreach (var game in rankings)
                {
                    var player = string.IsNullOrWhiteSpace(game.PlayerName) ? "Unknown" : game.PlayerName;
                    var scoreText = (game.Score.HasValue) ? game.Score.Value.ToString() : "0";

                    var item = new ListViewItem(rank.ToString());
                    item.SubItems.Add(player);
                    item.SubItems.Add(scoreText);

                    ListViewRanking.Items.Add(item);
                    rank++;
                }

                AdjustColumnWidths();
            }

            // Handle any exceptions that occur during database access
            catch (Exception ex)
            {
                var li = new ListViewItem(new[] { "-", "Error loading the ranking list.", "-" });
                ListViewRanking.Items.Add(li);
                AdjustColumnWidths();
                MessageBox.Show($"Error loading the ranking list:\n{ex.Message}\n\nINNER:\n{ex.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

            int nameWidth = clientWidth - rankWidth - scoreWidth - 4; // small padding

            // Adjust for scrollbar if needed
            if (ListViewRanking.Items.Count > ListViewRanking.ClientSize.Height / (int)Math.Max(1, ListViewRanking.Font.Height))
            {
                nameWidth -= scrollbarWidth;
            }

            // Ensure name column has a minimum width
            if (nameWidth < 50) nameWidth = 50;

            colRank.Width = rankWidth;
            colName.Width = nameWidth;
            colScore.Width = scoreWidth;
        }

        // Handle Stop button click
        private void StopButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
