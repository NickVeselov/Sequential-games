namespace SequentialGames
{
    partial class ParametersSettingsForm
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
            this.MainParamTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AdditionalPanel = new System.Windows.Forms.Panel();
            this.G = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MP_InitTB = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.finishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.AdditionalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.G)).BeginInit();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainParamTB
            // 
            this.MainParamTB.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainParamTB.Location = new System.Drawing.Point(178, 36);
            this.MainParamTB.Name = "MainParamTB";
            this.MainParamTB.Size = new System.Drawing.Size(127, 32);
            this.MainParamTB.TabIndex = 0;
            this.MainParamTB.Text = "Profit";
            this.MainParamTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Main parameter:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Additional";
            // 
            // AdditionalPanel
            // 
            this.AdditionalPanel.BackColor = System.Drawing.Color.Silver;
            this.AdditionalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdditionalPanel.Controls.Add(this.G);
            this.AdditionalPanel.Controls.Add(this.panel2);
            this.AdditionalPanel.Location = new System.Drawing.Point(0, 104);
            this.AdditionalPanel.Name = "AdditionalPanel";
            this.AdditionalPanel.Size = new System.Drawing.Size(414, 157);
            this.AdditionalPanel.TabIndex = 3;
            // 
            // G
            // 
            this.G.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.G.Location = new System.Drawing.Point(28, 49);
            this.G.Name = "G";
            this.G.Size = new System.Drawing.Size(259, 86);
            this.G.TabIndex = 4;
            this.G.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.G_CellEndEdit);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(11, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(135, 27);
            this.panel2.TabIndex = 3;
            // 
            // MP_InitTB
            // 
            this.MP_InitTB.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MP_InitTB.Location = new System.Drawing.Point(322, 36);
            this.MP_InitTB.Name = "MP_InitTB";
            this.MP_InitTB.Size = new System.Drawing.Size(61, 32);
            this.MP_InitTB.TabIndex = 4;
            this.MP_InitTB.Text = "(P)";
            this.MP_InitTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DimGray;
            this.menuStrip1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finishToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(414, 26);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // finishToolStripMenuItem
            // 
            this.finishToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.finishToolStripMenuItem.Name = "finishToolStripMenuItem";
            this.finishToolStripMenuItem.Size = new System.Drawing.Size(52, 22);
            this.finishToolStripMenuItem.Text = "Save";
            this.finishToolStripMenuItem.Click += new System.EventHandler(this.finishToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(331, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Variable";
            // 
            // ParametersSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(414, 261);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MP_InitTB);
            this.Controls.Add(this.AdditionalPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainParamTB);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ParametersSettingsForm";
            this.Text = "Parameters";
            this.AdditionalPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.G)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MainParamTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel AdditionalPanel;
        //Debug//
        public System.Windows.Forms.DataGridView G;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox MP_InitTB;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem finishToolStripMenuItem;
        private System.Windows.Forms.Label label3;
    }
}