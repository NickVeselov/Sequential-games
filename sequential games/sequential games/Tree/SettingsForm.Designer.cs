namespace SequentialGames
{
    partial class Settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PIBGrid = new System.Windows.Forms.DataGridView();
            this.IBPanel = new System.Windows.Forms.Panel();
            this.NamesPanel = new System.Windows.Forms.Panel();
            this.PlayersGrid = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.PIBGrid)).BeginInit();
            this.IBPanel.SuspendLayout();
            this.NamesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Players unlogical behaviour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(334, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "\r\n(Probability of picking unpleasant strategy alternative)";
            // 
            // PIBGrid
            // 
            this.PIBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PIBGrid.Location = new System.Drawing.Point(22, 49);
            this.PIBGrid.Name = "PIBGrid";
            this.PIBGrid.Size = new System.Drawing.Size(234, 32);
            this.PIBGrid.TabIndex = 6;
            this.PIBGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PIBGrid_CellEndEdit);
            // 
            // IBPanel
            // 
            this.IBPanel.BackColor = System.Drawing.Color.White;
            this.IBPanel.Controls.Add(this.label1);
            this.IBPanel.Controls.Add(this.PIBGrid);
            this.IBPanel.Controls.Add(this.label3);
            this.IBPanel.Location = new System.Drawing.Point(10, 138);
            this.IBPanel.Name = "IBPanel";
            this.IBPanel.Size = new System.Drawing.Size(363, 90);
            this.IBPanel.TabIndex = 7;
            // 
            // NamesPanel
            // 
            this.NamesPanel.BackColor = System.Drawing.Color.White;
            this.NamesPanel.Controls.Add(this.PlayersGrid);
            this.NamesPanel.Controls.Add(this.label2);
            this.NamesPanel.Location = new System.Drawing.Point(10, 38);
            this.NamesPanel.Name = "NamesPanel";
            this.NamesPanel.Size = new System.Drawing.Size(363, 90);
            this.NamesPanel.TabIndex = 8;
            // 
            // PlayersGrid
            // 
            this.PlayersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlayersGrid.Location = new System.Drawing.Point(12, 32);
            this.PlayersGrid.Name = "PlayersGrid";
            this.PlayersGrid.Size = new System.Drawing.Size(234, 32);
            this.PlayersGrid.TabIndex = 7;
            this.PlayersGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PlayersGrid_CellEndEdit);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Players";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(383, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Purple;
            this.ClientSize = new System.Drawing.Size(383, 239);
            this.Controls.Add(this.NamesPanel);
            this.Controls.Add(this.IBPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Settings";
            this.Text = "Settings";
            this.Resize += new System.EventHandler(this.Settings_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PIBGrid)).EndInit();
            this.IBPanel.ResumeLayout(false);
            this.IBPanel.PerformLayout();
            this.NamesPanel.ResumeLayout(false);
            this.NamesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView PIBGrid;
        private System.Windows.Forms.Panel IBPanel;
        private System.Windows.Forms.Panel NamesPanel;
//Debug//
        public System.Windows.Forms.DataGridView PlayersGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}