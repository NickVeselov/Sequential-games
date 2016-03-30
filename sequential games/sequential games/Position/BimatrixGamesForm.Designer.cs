namespace SequentialGames
{
    partial class BimatrixGamesForm
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
            this.plnum_panel = new System.Windows.Forms.Panel();
            this.n_label = new System.Windows.Forms.Label();
            this.n_box = new System.Windows.Forms.TextBox();
            this.strat_panel = new System.Windows.Forms.Panel();
            this.strat_grid_view = new System.Windows.Forms.DataGridView();
            this.payoff_panel = new System.Windows.Forms.Panel();
            this.vs_lb = new System.Windows.Forms.Label();
            this.pl2_lb = new System.Windows.Forms.Label();
            this.pl1_lb = new System.Windows.Forms.Label();
            this.B = new System.Windows.Forms.DataGridView();
            this.A = new System.Windows.Forms.DataGridView();
            this.navigation_panel = new System.Windows.Forms.Panel();
            this.Hint = new System.Windows.Forms.Label();
            this.done_btn = new System.Windows.Forms.Button();
            this.errormsg_strat = new System.Windows.Forms.Label();
            this.NavigationGrid = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.generalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.open_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.additionalParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelAsTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.plnum_panel.SuspendLayout();
            this.strat_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strat_grid_view)).BeginInit();
            this.payoff_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.B)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.A)).BeginInit();
            this.navigation_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NavigationGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plnum_panel
            // 
            this.plnum_panel.BackColor = System.Drawing.Color.NavajoWhite;
            this.plnum_panel.Controls.Add(this.n_label);
            this.plnum_panel.Controls.Add(this.n_box);
            this.plnum_panel.Location = new System.Drawing.Point(6, 32);
            this.plnum_panel.Name = "plnum_panel";
            this.plnum_panel.Size = new System.Drawing.Size(422, 56);
            this.plnum_panel.TabIndex = 0;
            // 
            // n_label
            // 
            this.n_label.AutoSize = true;
            this.n_label.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.n_label.Location = new System.Drawing.Point(187, 17);
            this.n_label.Name = "n_label";
            this.n_label.Size = new System.Drawing.Size(83, 24);
            this.n_label.TabIndex = 1;
            this.n_label.Text = "players";
            // 
            // n_box
            // 
            this.n_box.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.n_box.Location = new System.Drawing.Point(131, 14);
            this.n_box.MaxLength = 2;
            this.n_box.Name = "n_box";
            this.n_box.Size = new System.Drawing.Size(50, 32);
            this.n_box.TabIndex = 0;
            this.n_box.Text = "3";
            this.n_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.n_box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.n_box_KeyDown);
            this.n_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.n_box_KeyPress);
            this.n_box.Leave += new System.EventHandler(this.n_box_Leave);
            // 
            // strat_panel
            // 
            this.strat_panel.BackColor = System.Drawing.Color.NavajoWhite;
            this.strat_panel.Controls.Add(this.strat_grid_view);
            this.strat_panel.Location = new System.Drawing.Point(6, 94);
            this.strat_panel.Name = "strat_panel";
            this.strat_panel.Size = new System.Drawing.Size(422, 90);
            this.strat_panel.TabIndex = 1;
            // 
            // strat_grid_view
            // 
            this.strat_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.strat_grid_view.Location = new System.Drawing.Point(19, 28);
            this.strat_grid_view.Name = "strat_grid_view";
            this.strat_grid_view.Size = new System.Drawing.Size(377, 32);
            this.strat_grid_view.TabIndex = 0;
            this.strat_grid_view.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.strat_grid_view_CellEndEdit);
            // 
            // payoff_panel
            // 
            this.payoff_panel.BackColor = System.Drawing.Color.NavajoWhite;
            this.payoff_panel.Controls.Add(this.vs_lb);
            this.payoff_panel.Controls.Add(this.pl2_lb);
            this.payoff_panel.Controls.Add(this.pl1_lb);
            this.payoff_panel.Controls.Add(this.B);
            this.payoff_panel.Controls.Add(this.A);
            this.payoff_panel.Location = new System.Drawing.Point(6, 190);
            this.payoff_panel.Name = "payoff_panel";
            this.payoff_panel.Size = new System.Drawing.Size(422, 114);
            this.payoff_panel.TabIndex = 3;
            // 
            // vs_lb
            // 
            this.vs_lb.AutoSize = true;
            this.vs_lb.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vs_lb.Location = new System.Drawing.Point(178, 7);
            this.vs_lb.Name = "vs_lb";
            this.vs_lb.Size = new System.Drawing.Size(39, 24);
            this.vs_lb.TabIndex = 8;
            this.vs_lb.Text = "vs.";
            // 
            // pl2_lb
            // 
            this.pl2_lb.AutoSize = true;
            this.pl2_lb.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pl2_lb.Location = new System.Drawing.Point(292, 7);
            this.pl2_lb.Name = "pl2_lb";
            this.pl2_lb.Size = new System.Drawing.Size(72, 24);
            this.pl2_lb.TabIndex = 7;
            this.pl2_lb.Text = "Player";
            // 
            // pl1_lb
            // 
            this.pl1_lb.AutoSize = true;
            this.pl1_lb.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pl1_lb.Location = new System.Drawing.Point(31, 7);
            this.pl1_lb.Name = "pl1_lb";
            this.pl1_lb.Size = new System.Drawing.Size(72, 24);
            this.pl1_lb.TabIndex = 6;
            this.pl1_lb.Text = "Player";
            // 
            // B
            // 
            this.B.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.B.Location = new System.Drawing.Point(266, 34);
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(130, 60);
            this.B.TabIndex = 3;
            this.B.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Payoff_CellEndEdit);
            // 
            // A
            // 
            this.A.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.A.Location = new System.Drawing.Point(8, 34);
            this.A.Name = "A";
            this.A.Size = new System.Drawing.Size(130, 60);
            this.A.TabIndex = 0;
            this.A.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Payoff_CellEndEdit);
            // 
            // navigation_panel
            // 
            this.navigation_panel.BackColor = System.Drawing.Color.NavajoWhite;
            this.navigation_panel.Controls.Add(this.Hint);
            this.navigation_panel.Controls.Add(this.done_btn);
            this.navigation_panel.Controls.Add(this.errormsg_strat);
            this.navigation_panel.Controls.Add(this.NavigationGrid);
            this.navigation_panel.Location = new System.Drawing.Point(6, 310);
            this.navigation_panel.Name = "navigation_panel";
            this.navigation_panel.Size = new System.Drawing.Size(422, 182);
            this.navigation_panel.TabIndex = 4;
            // 
            // Hint
            // 
            this.Hint.AutoSize = true;
            this.Hint.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hint.Location = new System.Drawing.Point(6, 78);
            this.Hint.Name = "Hint";
            this.Hint.Size = new System.Drawing.Size(163, 42);
            this.Hint.TabIndex = 6;
            this.Hint.Text = "Click on the cell\r\nto view matrixes\r\n";
            // 
            // done_btn
            // 
            this.done_btn.BackColor = System.Drawing.Color.SaddleBrown;
            this.done_btn.Enabled = false;
            this.done_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.done_btn.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.done_btn.ForeColor = System.Drawing.Color.DarkGray;
            this.done_btn.Location = new System.Drawing.Point(276, 116);
            this.done_btn.Name = "done_btn";
            this.done_btn.Size = new System.Drawing.Size(140, 60);
            this.done_btn.TabIndex = 1;
            this.done_btn.Text = "Some matrixes\r\nare empty";
            this.done_btn.UseVisualStyleBackColor = false;
            this.done_btn.Click += new System.EventHandler(this.done_btn_Click);
            // 
            // errormsg_strat
            // 
            this.errormsg_strat.AutoSize = true;
            this.errormsg_strat.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errormsg_strat.Location = new System.Drawing.Point(178, 15);
            this.errormsg_strat.Name = "errormsg_strat";
            this.errormsg_strat.Size = new System.Drawing.Size(198, 48);
            this.errormsg_strat.TabIndex = 5;
            this.errormsg_strat.Text = "No Strategies data\r\nfor player";
            this.errormsg_strat.Visible = false;
            // 
            // NavigationGrid
            // 
            this.NavigationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NavigationGrid.Location = new System.Drawing.Point(8, 15);
            this.NavigationGrid.Name = "NavigationGrid";
            this.NavigationGrid.Size = new System.Drawing.Size(130, 60);
            this.NavigationGrid.TabIndex = 4;
            this.NavigationGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.navigation_grid_CellDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.OldLace;
            this.menuStrip1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalToolStripMenuItem,
            this.additionalParametersToolStripMenuItem,
            this.parametersToolStripMenuItem,
            this.modelAsTreeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(434, 26);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // generalToolStripMenuItem
            // 
            this.generalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.open_menu,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
            this.generalToolStripMenuItem.Size = new System.Drawing.Size(45, 22);
            this.generalToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // open_menu
            // 
            this.open_menu.Name = "open_menu";
            this.open_menu.Size = new System.Drawing.Size(110, 22);
            this.open_menu.Text = "Open";
            this.open_menu.Click += new System.EventHandler(this.open_menu_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // additionalParametersToolStripMenuItem
            // 
            this.additionalParametersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.calculateValuesToolStripMenuItem});
            this.additionalParametersToolStripMenuItem.Name = "additionalParametersToolStripMenuItem";
            this.additionalParametersToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.additionalParametersToolStripMenuItem.Text = "Additional parameters";
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            this.setToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.setToolStripMenuItem.Text = "Set";
            this.setToolStripMenuItem.Click += new System.EventHandler(this.setToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // calculateValuesToolStripMenuItem
            // 
            this.calculateValuesToolStripMenuItem.Name = "calculateValuesToolStripMenuItem";
            this.calculateValuesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.calculateValuesToolStripMenuItem.Text = "Calculate values";
            this.calculateValuesToolStripMenuItem.Click += new System.EventHandler(this.calculateValuesToolStripMenuItem_Click);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.parametersToolStripMenuItem.Text = "Parameters";
            this.parametersToolStripMenuItem.Visible = false;
            // 
            // modelAsTreeToolStripMenuItem
            // 
            this.modelAsTreeToolStripMenuItem.Name = "modelAsTreeToolStripMenuItem";
            this.modelAsTreeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.modelAsTreeToolStripMenuItem.Text = "Model as Tree";
            this.modelAsTreeToolStripMenuItem.Visible = false;
            this.modelAsTreeToolStripMenuItem.Click += new System.EventHandler(this.modelAsTreeToolStripMenuItem_Click);
            // 
            // BimatrixGamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.ClientSize = new System.Drawing.Size(434, 498);
            this.Controls.Add(this.navigation_panel);
            this.Controls.Add(this.payoff_panel);
            this.Controls.Add(this.strat_panel);
            this.Controls.Add(this.plnum_panel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BimatrixGamesForm";
            this.Text = "Game position data input";
            this.Resize += new System.EventHandler(this.BimatrixGamesForm_Resize);
            this.plnum_panel.ResumeLayout(false);
            this.plnum_panel.PerformLayout();
            this.strat_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.strat_grid_view)).EndInit();
            this.payoff_panel.ResumeLayout(false);
            this.payoff_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.B)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.A)).EndInit();
            this.navigation_panel.ResumeLayout(false);
            this.navigation_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NavigationGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plnum_panel;
        private System.Windows.Forms.Panel strat_panel;
        private System.Windows.Forms.Label n_label;
        private System.Windows.Forms.TextBox n_box;

//Debug//
        public System.Windows.Forms.DataGridView strat_grid_view;
        private System.Windows.Forms.Panel payoff_panel;
        private System.Windows.Forms.DataGridView A;
        private System.Windows.Forms.DataGridView B;
        private System.Windows.Forms.Panel navigation_panel;
        private System.Windows.Forms.Button done_btn;
        private System.Windows.Forms.DataGridView NavigationGrid;
        private System.Windows.Forms.Label errormsg_strat;
        private System.Windows.Forms.Label pl1_lb;
        private System.Windows.Forms.Label pl2_lb;
        private System.Windows.Forms.Label vs_lb;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem generalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem open_menu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem modelAsTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem additionalParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.Label Hint;
        private System.Windows.Forms.ToolStripMenuItem calculateValuesToolStripMenuItem;
    }
}