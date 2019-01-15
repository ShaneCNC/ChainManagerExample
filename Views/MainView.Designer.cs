namespace ChainManagerExample.Views
{
    partial class MainView
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
            this.ChainLevelButton = new System.Windows.Forms.Button();
            this.ChainAllSelectedBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChainLevelButton
            // 
            this.ChainLevelButton.Location = new System.Drawing.Point(12, 12);
            this.ChainLevelButton.Name = "ChainLevelButton";
            this.ChainLevelButton.Size = new System.Drawing.Size(188, 30);
            this.ChainLevelButton.TabIndex = 1;
            this.ChainLevelButton.Text = "Chain Level";
            this.ChainLevelButton.UseVisualStyleBackColor = true;
            this.ChainLevelButton.Click += new System.EventHandler(this.OnChainLevel);
            // 
            // ChainAllSelectedBtn
            // 
            this.ChainAllSelectedBtn.Location = new System.Drawing.Point(12, 48);
            this.ChainAllSelectedBtn.Name = "ChainAllSelectedBtn";
            this.ChainAllSelectedBtn.Size = new System.Drawing.Size(188, 30);
            this.ChainAllSelectedBtn.TabIndex = 2;
            this.ChainAllSelectedBtn.Text = "Chain All Selected";
            this.ChainAllSelectedBtn.UseVisualStyleBackColor = true;
            this.ChainAllSelectedBtn.Click += new System.EventHandler(this.OnChainAllSelected);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "Chain Geometry";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnChainGeometry);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(151, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnCloseView);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 180);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ChainAllSelectedBtn);
            this.Controls.Add(this.ChainLevelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chain Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ChainLevelButton;
        private System.Windows.Forms.Button ChainAllSelectedBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}