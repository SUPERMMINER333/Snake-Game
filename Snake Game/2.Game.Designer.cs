namespace Snake_Game
{
    partial class Game
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            picCanvas = new PictureBox();
            txtScore = new Label();
            txtHighScore = new Label();
            StopButton = new Button();
            gameTime = new System.Windows.Forms.Timer(components);
            RefreshButton = new Button();
            PlayerNameTextBox = new TextBox();
            PlayerNameText = new Label();
            RipLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)picCanvas).BeginInit();
            SuspendLayout();
            // 
            // picCanvas
            // 
            picCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picCanvas.BackColor = Color.Silver;
            picCanvas.Location = new Point(12, 88);
            picCanvas.Name = "picCanvas";
            picCanvas.Size = new Size(758, 653);
            picCanvas.TabIndex = 0;
            picCanvas.TabStop = false;
            picCanvas.Paint += UpdateGameBoard;
            // 
            // txtScore
            // 
            txtScore.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtScore.AutoSize = true;
            txtScore.BackColor = Color.Transparent;
            txtScore.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtScore.Location = new Point(12, 9);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(121, 38);
            txtScore.TabIndex = 1;
            txtScore.Text = "Score: 0";
            // 
            // txtHighScore
            // 
            txtHighScore.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtHighScore.BackColor = Color.Transparent;
            txtHighScore.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtHighScore.Location = new Point(12, 42);
            txtHighScore.Name = "txtHighScore";
            txtHighScore.Size = new Size(297, 43);
            txtHighScore.TabIndex = 1;
            txtHighScore.Text = "High Score: 0";
            txtHighScore.UseCompatibleTextRendering = true;
            // 
            // StopButton
            // 
            StopButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StopButton.BackColor = Color.Red;
            StopButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            StopButton.Location = new Point(450, 9);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(320, 76);
            StopButton.TabIndex = 2;
            StopButton.Text = "Exit";
            StopButton.UseVisualStyleBackColor = false;
            StopButton.Click += StopGame;
            // 
            // gameTime
            // 
            gameTime.Interval = 40;
            gameTime.Tick += GameTimer;
            // 
            // RefreshButton
            // 
            RefreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RefreshButton.BackColor = Color.LimeGreen;
            RefreshButton.BackgroundImage = Properties.Resources.rsz_3refresh;
            RefreshButton.BackgroundImageLayout = ImageLayout.Center;
            RefreshButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RefreshButton.Location = new Point(368, 9);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(76, 76);
            RefreshButton.TabIndex = 3;
            RefreshButton.UseVisualStyleBackColor = false;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // PlayerNameTextBox
            // 
            PlayerNameTextBox.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PlayerNameTextBox.Location = new Point(119, 386);
            PlayerNameTextBox.Name = "PlayerNameTextBox";
            PlayerNameTextBox.Size = new Size(544, 65);
            PlayerNameTextBox.TabIndex = 4;
            PlayerNameTextBox.KeyUp += SavaPlayerName;
            // 
            // PlayerNameText
            // 
            PlayerNameText.AutoSize = true;
            PlayerNameText.BackColor = Color.Silver;
            PlayerNameText.Font = new Font("Segoe UI", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PlayerNameText.Location = new Point(252, 309);
            PlayerNameText.Name = "PlayerNameText";
            PlayerNameText.Size = new Size(319, 62);
            PlayerNameText.TabIndex = 5;
            PlayerNameText.Text = "Player Name:";
            // 
            // RipLabel
            // 
            RipLabel.AutoSize = true;
            RipLabel.BackColor = Color.Red;
            RipLabel.Font = new Font("Impact", 72F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RipLabel.Location = new Point(291, 164);
            RipLabel.Name = "RipLabel";
            RipLabel.Size = new Size(224, 145);
            RipLabel.TabIndex = 6;
            RipLabel.Text = "RIP";
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 753);
            Controls.Add(RipLabel);
            Controls.Add(PlayerNameText);
            Controls.Add(PlayerNameTextBox);
            Controls.Add(RefreshButton);
            Controls.Add(StopButton);
            Controls.Add(txtHighScore);
            Controls.Add(txtScore);
            Controls.Add(picCanvas);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Game";
            Text = "Snake Game";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)picCanvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picCanvas;
        private Label txtScore;
        private Label txtHighScore;
        private Button StopButton;
        private System.Windows.Forms.Timer gameTime;
        private Button RefreshButton;
        private TextBox PlayerNameTextBox;
        private Label PlayerNameText;
        private Label RipLabel;
    }
}