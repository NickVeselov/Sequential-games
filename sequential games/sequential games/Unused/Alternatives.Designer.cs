namespace SequentialGames
{
    partial class Alternatives
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CritGrid = new System.Windows.Forms.DataGridView();
            this.CritLabel = new System.Windows.Forms.Label();
            this.CritPanel = new System.Windows.Forms.Panel();
            this.AltPanel = new System.Windows.Forms.Panel();
            this.DoneButton = new System.Windows.Forms.Button();
            this.AltGrid = new System.Windows.Forms.DataGridView();
            this.AltLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CritGrid)).BeginInit();
            this.CritPanel.SuspendLayout();
            this.AltPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AltGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Book Antiqua", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(541, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // CritGrid
            // 
            this.CritGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CritGrid.Location = new System.Drawing.Point(21, 37);
            this.CritGrid.Name = "CritGrid";
            this.CritGrid.Size = new System.Drawing.Size(488, 83);
            this.CritGrid.TabIndex = 1;
            this.CritGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CritGrid_CellEndEdit);
            this.CritGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CritGrid_CellMouseClick);
            // 
            // CritLabel
            // 
            this.CritLabel.AutoSize = true;
            this.CritLabel.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CritLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CritLabel.Location = new System.Drawing.Point(200, 10);
            this.CritLabel.Name = "CritLabel";
            this.CritLabel.Size = new System.Drawing.Size(125, 24);
            this.CritLabel.TabIndex = 3;
            this.CritLabel.Text = "Criteria list";
            // 
            // CritPanel
            // 
            this.CritPanel.Controls.Add(this.CritGrid);
            this.CritPanel.Controls.Add(this.CritLabel);
            this.CritPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CritPanel.Location = new System.Drawing.Point(0, 25);
            this.CritPanel.Name = "CritPanel";
            this.CritPanel.Size = new System.Drawing.Size(541, 181);
            this.CritPanel.TabIndex = 5;
            // 
            // AltPanel
            // 
            this.AltPanel.Controls.Add(this.DoneButton);
            this.AltPanel.Controls.Add(this.AltGrid);
            this.AltPanel.Controls.Add(this.AltLabel);
            this.AltPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AltPanel.Location = new System.Drawing.Point(0, 212);
            this.AltPanel.Name = "AltPanel";
            this.AltPanel.Size = new System.Drawing.Size(541, 207);
            this.AltPanel.TabIndex = 6;
            // 
            // DoneButton
            // 
            this.DoneButton.BackColor = System.Drawing.Color.Gold;
            this.DoneButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DoneButton.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoneButton.Location = new System.Drawing.Point(432, 159);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(97, 36);
            this.DoneButton.TabIndex = 7;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = false;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // AltGrid
            // 
            this.AltGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AltGrid.Location = new System.Drawing.Point(21, 52);
            this.AltGrid.Name = "AltGrid";
            this.AltGrid.Size = new System.Drawing.Size(488, 83);
            this.AltGrid.TabIndex = 1;
            this.AltGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.AltGrid_CellEndEdit);
            this.AltGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AltGrid_CellMouseClick);
            // 
            // AltLabel
            // 
            this.AltLabel.AutoSize = true;
            this.AltLabel.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AltLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.AltLabel.Location = new System.Drawing.Point(189, 25);
            this.AltLabel.Name = "AltLabel";
            this.AltLabel.Size = new System.Drawing.Size(169, 24);
            this.AltLabel.TabIndex = 3;
            this.AltLabel.Text = "Alternatives list";
            // 
            // Alternatives
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(541, 419);
            this.Controls.Add(this.AltPanel);
            this.Controls.Add(this.CritPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Alternatives";
            this.Text = "Alternatives";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CritGrid)).EndInit();
            this.CritPanel.ResumeLayout(false);
            this.CritPanel.PerformLayout();
            this.AltPanel.ResumeLayout(false);
            this.AltPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AltGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.DataGridView CritGrid;
        private System.Windows.Forms.Label CritLabel;
        private System.Windows.Forms.Panel CritPanel;
        private System.Windows.Forms.Panel AltPanel;
        private System.Windows.Forms.DataGridView AltGrid;
        private System.Windows.Forms.Label AltLabel;
        private System.Windows.Forms.Button DoneButton;
    }
}