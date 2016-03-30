namespace SequentialGames
{
    partial class PositionInformation_Form
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.choose_strat_btn = new System.Windows.Forms.Button();
            this.StratLabel = new System.Windows.Forms.Label();
            this.PayoffsLabel = new System.Windows.Forms.Label();
            this.CombinationsLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.summonModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optimalityCriterionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalProfitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.individualProfitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalLossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.individualLossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalMinumumExpencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.individualMinumumExpensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(12, 255);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 24);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Sub tree";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // choose_strat_btn
            // 
            this.choose_strat_btn.BackColor = System.Drawing.Color.Sienna;
            this.choose_strat_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.choose_strat_btn.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choose_strat_btn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.choose_strat_btn.Location = new System.Drawing.Point(40, 168);
            this.choose_strat_btn.Name = "choose_strat_btn";
            this.choose_strat_btn.Size = new System.Drawing.Size(139, 52);
            this.choose_strat_btn.TabIndex = 2;
            this.choose_strat_btn.Text = "Choose optimal Strategies";
            this.choose_strat_btn.UseVisualStyleBackColor = false;
            this.choose_strat_btn.Visible = false;
            // 
            // StratLabel
            // 
            this.StratLabel.AutoSize = true;
            this.StratLabel.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StratLabel.Location = new System.Drawing.Point(51, 43);
            this.StratLabel.Name = "StratLabel";
            this.StratLabel.Size = new System.Drawing.Size(196, 24);
            this.StratLabel.TabIndex = 3;
            this.StratLabel.Text = "Optimal strategies";
            this.StratLabel.Visible = false;
            // 
            // PayoffsLabel
            // 
            this.PayoffsLabel.AutoSize = true;
            this.PayoffsLabel.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PayoffsLabel.Location = new System.Drawing.Point(51, 91);
            this.PayoffsLabel.Name = "PayoffsLabel";
            this.PayoffsLabel.Size = new System.Drawing.Size(171, 24);
            this.PayoffsLabel.TabIndex = 4;
            this.PayoffsLabel.Text = "Optimal payoffs";
            this.PayoffsLabel.Visible = false;
            // 
            // CombinationsLabel
            // 
            this.CombinationsLabel.AutoSize = true;
            this.CombinationsLabel.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CombinationsLabel.Location = new System.Drawing.Point(-19, 67);
            this.CombinationsLabel.Name = "CombinationsLabel";
            this.CombinationsLabel.Size = new System.Drawing.Size(370, 24);
            this.CombinationsLabel.TabIndex = 5;
            this.CombinationsLabel.Text = "Most likely strategies combinations";
            this.CombinationsLabel.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.BurlyWood;
            this.menuStrip1.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.summonModelToolStripMenuItem,
            this.optimalityCriterionToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(332, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // summonModelToolStripMenuItem
            // 
            this.summonModelToolStripMenuItem.Name = "summonModelToolStripMenuItem";
            this.summonModelToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.summonModelToolStripMenuItem.Text = "Create bimatrix model";
            this.summonModelToolStripMenuItem.Click += new System.EventHandler(this.create_model_btn_Click);
            // 
            // optimalityCriterionToolStripMenuItem
            // 
            this.optimalityCriterionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalProfitToolStripMenuItem,
            this.individualProfitToolStripMenuItem,
            this.totalLossToolStripMenuItem,
            this.individualLossToolStripMenuItem,
            this.totalMinumumExpencesToolStripMenuItem,
            this.individualMinumumExpensesToolStripMenuItem});
            this.optimalityCriterionToolStripMenuItem.Name = "optimalityCriterionToolStripMenuItem";
            this.optimalityCriterionToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.optimalityCriterionToolStripMenuItem.Text = "Optimality criterion";
            this.optimalityCriterionToolStripMenuItem.Visible = false;
            // 
            // totalProfitToolStripMenuItem
            // 
            this.totalProfitToolStripMenuItem.Name = "totalProfitToolStripMenuItem";
            this.totalProfitToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.totalProfitToolStripMenuItem.Text = "Total profit";
            this.totalProfitToolStripMenuItem.Click += new System.EventHandler(this.ChangeOptimalityCriterion);
            // 
            // individualProfitToolStripMenuItem
            // 
            this.individualProfitToolStripMenuItem.Name = "individualProfitToolStripMenuItem";
            this.individualProfitToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.individualProfitToolStripMenuItem.Text = "Individual profit";
            this.individualProfitToolStripMenuItem.Click += new System.EventHandler(this.ChangeOptimalityCriterion);
            // 
            // totalLossToolStripMenuItem
            // 
            this.totalLossToolStripMenuItem.Name = "totalLossToolStripMenuItem";
            this.totalLossToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.totalLossToolStripMenuItem.Text = "Total loss";
            this.totalLossToolStripMenuItem.Click += new System.EventHandler(this.ChangeOptimalityCriterion);
            // 
            // individualLossToolStripMenuItem
            // 
            this.individualLossToolStripMenuItem.Name = "individualLossToolStripMenuItem";
            this.individualLossToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.individualLossToolStripMenuItem.Text = "Individual loss";
            this.individualLossToolStripMenuItem.Click += new System.EventHandler(this.ChangeOptimalityCriterion);
            // 
            // totalMinumumExpencesToolStripMenuItem
            // 
            this.totalMinumumExpencesToolStripMenuItem.Name = "totalMinumumExpencesToolStripMenuItem";
            this.totalMinumumExpencesToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.totalMinumumExpencesToolStripMenuItem.Text = "Total minumum expenses";
            this.totalMinumumExpencesToolStripMenuItem.Click += new System.EventHandler(this.ChangeOptimalityCriterion);
            // 
            // individualMinumumExpensesToolStripMenuItem
            // 
            this.individualMinumumExpensesToolStripMenuItem.Name = "individualMinumumExpensesToolStripMenuItem";
            this.individualMinumumExpensesToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.individualMinumumExpensesToolStripMenuItem.Text = "Individual minumum expenses";
            this.individualMinumumExpensesToolStripMenuItem.Click += new System.EventHandler(this.ChangeOptimalityCriterion);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // PositionInformation_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(332, 161);
            this.Controls.Add(this.choose_strat_btn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.StratLabel);
            this.Controls.Add(this.PayoffsLabel);
            this.Controls.Add(this.CombinationsLabel);
            this.Controls.Add(this.menuStrip1);
            this.Name = "PositionInformation_Form";
            this.Text = "\"PositionName\" position";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PositionInformation_Form_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button choose_strat_btn;
        private System.Windows.Forms.Label StratLabel;
        private System.Windows.Forms.Label PayoffsLabel;
        private System.Windows.Forms.Label CombinationsLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem summonModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optimalityCriterionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalProfitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem individualProfitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalLossToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem individualLossToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalMinumumExpencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem individualMinumumExpensesToolStripMenuItem;
    }
}