namespace Snake_Game
{
    partial class RankingList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TitelLabel = new Label();
            ListViewRanking = new ListView();
            colRank = new ColumnHeader();
            colName = new ColumnHeader();
            colScore = new ColumnHeader();
            StopButton = new Button();
            SuspendLayout();
            // 
            // TitelLabel
            // 
            TitelLabel.AutoSize = true;
            TitelLabel.Font = new Font("Segoe UI", 48F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            TitelLabel.Location = new Point(142, 9);
            TitelLabel.Name = "TitelLabel";
            TitelLabel.Size = new Size(506, 106);
            TitelLabel.TabIndex = 0;
            TitelLabel.Text = "Ranking List";
            // 
            // ListViewRanking
            // 
            ListViewRanking.Columns.AddRange(new ColumnHeader[] { colRank, colName, colScore });
            ListViewRanking.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ListViewRanking.FullRowSelect = true;
            ListViewRanking.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            ListViewRanking.Location = new Point(12, 127);
            ListViewRanking.Name = "ListViewRanking";
            ListViewRanking.Size = new Size(758, 614);
            ListViewRanking.TabIndex = 1;
            ListViewRanking.UseCompatibleStateImageBehavior = false;
            ListViewRanking.View = View.Details;
            // 
            // colRank
            // 
            colRank.Text = "Place";
            colRank.Width = 120;
            // 
            // colName
            // 
            colName.Text = "Player name";
            colName.TextAlign = HorizontalAlignment.Center;
            colName.Width = 515;
            // 
            // colScore
            // 
            colScore.Text = "Score";
            colScore.TextAlign = HorizontalAlignment.Right;
            colScore.Width = 120;
            // 
            // StopButton
            // 
            StopButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StopButton.BackColor = Color.Red;
            StopButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            StopButton.Location = new Point(620, 681);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(150, 60);
            StopButton.TabIndex = 2;
            StopButton.Text = "Back";
            StopButton.UseVisualStyleBackColor = false;
            StopButton.Click += StopButton_Click;
            // 
            // RankingList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 753);
            Controls.Add(StopButton);
            Controls.Add(ListViewRanking);
            Controls.Add(TitelLabel);
            Name = "RankingList";
            Text = "Snake Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TitelLabel;
        private ListView ListViewRanking;
        private ColumnHeader colRank;
        private ColumnHeader colName;
        private ColumnHeader colScore;
        private Button StopButton;
    }
}