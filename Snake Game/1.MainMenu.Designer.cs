namespace Snake_Game
{
    partial class MainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            StartButton = new Button();
            StopButton = new Button();
            label1 = new Label();
            StatsButton = new Button();
            SuspendLayout();
            // 
            // StartButton
            // 
            StartButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StartButton.BackColor = Color.LawnGreen;
            StartButton.Font = new Font("Segoe UI", 18F);
            StartButton.Location = new Point(240, 360);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(320, 80);
            StartButton.TabIndex = 0;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartButton_Click;
            // 
            // StopButton
            // 
            StopButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StopButton.BackColor = Color.Red;
            StopButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            StopButton.Location = new Point(240, 532);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(320, 80);
            StopButton.TabIndex = 0;
            StopButton.Text = "Exit";
            StopButton.UseVisualStyleBackColor = false;
            StopButton.Click += StopButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(97, 201);
            label1.Name = "label1";
            label1.Size = new Size(619, 60);
            label1.TabIndex = 2;
            label1.Text = "Welcome to the Snake Game";
            // 
            // StatsButton
            // 
            StatsButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StatsButton.BackColor = Color.Gold;
            StatsButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            StatsButton.Location = new Point(240, 446);
            StatsButton.Name = "StatsButton";
            StatsButton.Size = new Size(320, 80);
            StatsButton.TabIndex = 3;
            StatsButton.Text = "Ranking List";
            StatsButton.UseVisualStyleBackColor = false;
            StatsButton.Click += StatsButton_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 753);
            Controls.Add(StatsButton);
            Controls.Add(label1);
            Controls.Add(StartButton);
            Controls.Add(StopButton);
            Name = "MainMenu";
            Text = "Snake Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartButton;
        private Button StopButton;
        private Label label1;
        private Button StatsButton;
    }
}
