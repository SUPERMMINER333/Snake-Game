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
        private void LoadRankingList()
        {
            ListViewRanking.Items.Clear();

            try
            {
                using var db = new Models.SnakeGameContext();

                if (!db.Database.CanConnect())
                {
                    var li = new ListViewItem(new[] { "-", "Keine Verbindung zur Datenbank.", "-" });
                    ListViewRanking.Items.Add(li);
                    AdjustColumnWidths();
                    return;
                }

                var rankings = db.SnakeGames
                                 .OrderByDescending(s => s.Score)
                                 .Take(10)
                                 .ToList();

                if (rankings.Count == 0)
                {
                    var li = new ListViewItem(new[] { "-", "Keine Einträge in der Datenbank.", "-" });
                    ListViewRanking.Items.Add(li);
                    AdjustColumnWidths();
                    return;
                }

                int rank = 1;

                foreach (var game in rankings)
                {
                    var player = string.IsNullOrWhiteSpace(game.PlayerName) ? "Unbekannt" : game.PlayerName;
                    var scoreText = (game.Score.HasValue) ? game.Score.Value.ToString() : "0";

                    var item = new ListViewItem(rank.ToString());
                    item.SubItems.Add(player);
                    item.SubItems.Add(scoreText);

                    ListViewRanking.Items.Add(item);
                    rank++;
                }

                AdjustColumnWidths();
            }
            catch (Exception ex)
            {
                var li = new ListViewItem(new[] { "-", "Fehler beim Laden der Rangliste.", "-" });
                ListViewRanking.Items.Add(li);
                AdjustColumnWidths();
                MessageBox.Show(
                    $"Fehler beim Laden der Rangliste:\n{ex.Message}\n\nINNER:\n{ex.InnerException?.Message}",
                    "Fehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        private void AdjustColumnWidths()
        {
            // Ensure columns use the available client width and account for a possible vertical scrollbar
            if (ListViewRanking == null) return;

            int clientWidth = ListViewRanking.ClientSize.Width;
            int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;

            // Keep rank and score columns fixed, name column fills the remaining space
            int rankWidth = Math.Max(80, colRank.Width);
            int scoreWidth = Math.Max(80, colScore.Width);

            int nameWidth = clientWidth - rankWidth - scoreWidth - 4; // small padding

            // If items need a scrollbar, subtract scrollbar width
            if (ListViewRanking.Items.Count > ListViewRanking.ClientSize.Height / (int)Math.Max(1, ListViewRanking.Font.Height))
            {
                nameWidth -= scrollbarWidth;
            }

            if (nameWidth < 50) nameWidth = 50;

            colRank.Width = rankWidth;
            colName.Width = nameWidth;
            colScore.Width = scoreWidth;
        }
    }
}
