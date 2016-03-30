using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SequentialGames
{
    public partial class PositionInformation_Form : Form
    {
        public GamePosition gp;
        Tree parent;
        public DataGridView DgCombinations = new DataGridView();
        DataGridView DgPayoffs = new DataGridView();
        DataGridView DgStrategies = new DataGridView();
        public List<DataGridView> DgUtilies = new List<DataGridView>();
        bool DataChanging = false;

        public PositionInformation_Form(Tree parent_input, GamePosition gp_input)
        {
            InitializeComponent();
            parent = parent_input;
            gp = gp_input;

            if (gp.defined)
            {               
                if (gp.LHStrategy.Count == 0)
                    SolveGame(gp);
                else
                    show_result();
            }
        }

        public void SolveGame(GamePosition gp_input)
        {
            gp.N = gp_input.N;
            gp.Strategies = gp_input.Strategies;
            gp.payoffs = gp_input.payoffs;
            gp.AdParamValues = gp_input.AdParamValues;

            for (int i = 0; i < gp.N; i++)
            {
                gp.V.Add(new List<double>());
                gp.LHStrategy.Add(new List<List<double>>());
                gp.OptimalStrategy_R.Add(new List<List<double>>());
                for (int j = 0; j < gp.N; j++)
                {
                    gp.V[i].Add(0);
                    gp.LHStrategy[i].Add(new List<double>());
                    gp.OptimalStrategy_R[i].Add(new List<double>());
                }
            }

            gp.Utility.Clear();
            for (int i = 0; i < gp.N; i++)
            {
                gp.Utility.Add(new List<List<List<double>>>());
                for (int j = 0; j < gp.N; j++)
                    gp.Utility[i].Add(new List<List<double>>());
            }

            for (int i = 0; i < gp.N; i++)
                for (int j = 0; j < gp.N; j++)
                {
                    gp.Utility[i].Add(new List<List<double>>());
                    if (i < j)
                    {
                        GameTheory.LemkeHowson LH = new GameTheory.LemkeHowson(gp.Strategies[i], gp.Strategies[j]);
                        gp.CalculatePayoffsValues();

                        LH.A = gp.CreateUtilityPayoffMatrix(i, j);
                        LH.B = gp.CreateUtilityPayoffMatrix(j, i);
                        gp.Utility[i][j] = LH.A;
                        gp.Utility[j][i] = LH.B;
                        //DebugForm d = new DebugForm(LH.A, LH.B);
                        //d.ShowDialog();

                        LH.salvation();
                        gp.V[i][j] = LH.prize_1;
                        gp.V[j][i] = LH.prize_2;

                        if ((LH.prize_1 == 0) && (LH.prize_2 == 0))
                        {
                            System.Windows.Forms.MessageBox.Show
                                ("Lemke-Howson got into endless loop. /nAlgorithm is not optimal");
                            this.Close();
                        }
                        else
                        {
                            for (int k = 0; k < gp.Strategies[i]; k++)
                            {
                                gp.LHStrategy[i][j].Add(0);
                                gp.OptimalStrategy_R[i][j].Add(0);
                            }
                            for (int k = 0; k < gp.Strategies[j]; k++)
                            {
                                gp.LHStrategy[j][i].Add(0);
                                gp.OptimalStrategy_R[j][i].Add(0);
                            }

                            for (int k = 0; k < LH.strat1.Count; k++)
                            {
                                gp.LHStrategy[i][j][k] = LH.strat1[k];
                                gp.OptimalStrategy_R[i][j][k] = LH.strat1[k];
                            }

                            for (int k = 0; k < LH.strat2.Count; k++)
                            {
                                gp.LHStrategy[j][i][k] = LH.strat2[k];
                                gp.OptimalStrategy_R[j][i][k] = LH.strat2[k];
                            }
                        }
                    }
                }

            gp.defined = true;

            show_result();
            menuStrip1.Show();
        }

        public void create_model_btn_Click(object sender, EventArgs e)
        {
            if ((gp.DataForm != null)&&(gp.DataForm.Visible))
                gp.DataForm.Focus();
            else
            {
                BimatrixGamesForm form = new BimatrixGamesForm(gp);
                //D//
                DebugClass.BG = form;
                GamePosition before = gp;
                gp.DataForm = form;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Text = this.Text;
                form.Tag = this;
                form.Show();
            }
        }

        private void ChangeOptimalityCriterion(object sender, System.EventArgs e)
        {
            (sender as ToolStripMenuItem).Checked = true;
            ToolStripMenuItem Active = sender as ToolStripMenuItem;

            for (int i = 0; i < optimalityCriterionToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (optimalityCriterionToolStripMenuItem.DropDownItems[i] != Active)
                    (optimalityCriterionToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }
        }

        private void create_strategies_grid(DataGridView dg_view)
        {
            this.Controls.Add(dg_view);

            //Correct number of elements in Irrational Behaviour
            if (Information.PlayersIrrationalBehaviour.Count < gp.N)
            {
                for (int j = Information.PlayersIrrationalBehaviour.Count; j < gp.N; j++)
                    Information.PlayersIrrationalBehaviour.Add(0);
            }


            //Calculate modified strategy vectors
            for (int i = 0; i < gp.N; i++)
            {
                double avg = 1.00 / gp.Strategies[i];
                for (int j = 0; j < gp.N; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < gp.LHStrategy[i][j].Count; k++)
                    {
                        if (gp.LHStrategy[i][j][k] < avg)
                            gp.OptimalStrategy_R[i][j][k] = gp.LHStrategy[i][j][k] +
                                Information.PlayersIrrationalBehaviour[i] * 2 * (avg - gp.LHStrategy[i][j][k]);
                        else
                            gp.OptimalStrategy_R[i][j][k] = gp.LHStrategy[i][j][k] *
                                (1 - Information.PlayersIrrationalBehaviour[i]);

                        sum += gp.OptimalStrategy_R[i][j][k];
                    }

                    for (int k = 0; k < gp.LHStrategy[i][j].Count; k++)
                        gp.OptimalStrategy_R[i][j][k] *= (1.00 / sum);
                }
            }

            Graphic_Interface.Grid dg = new Graphic_Interface.Grid(dg_view, gp.N, gp.N, "T", "T");
            dg.initialize();
            int InactiveCount = 0;
            for (int i = 0; i < Information.players_number; i++)
            {
                if (gp.ActivePlayers[i])
                {
                    if (Information.PlayersNames[i] == "")
                    {
                        dg_view.Rows[i - InactiveCount].HeaderCell.Value = "Player " + (i + 1).ToString();
                        dg_view.Columns[i - InactiveCount].HeaderCell.Value = "Player " + (i + 1).ToString();
                    }
                    else
                    {
                        dg_view.Rows[i - InactiveCount].HeaderCell.Value = Information.PlayersNames[i];
                        dg_view.Columns[i - InactiveCount].HeaderCell.Value = Information.PlayersNames[i];
                    }
                }
                else
                    InactiveCount++;
            }
            dg.create_headers();
            dg_view.TopLeftHeaderCell.Value = "Strategies";

            for (int i = 0; i < gp.N; i++)
                for (int j = 0; j < gp.N; j++)
                {
                    if (i != j)
                    {
                        string str = "[ ";
                        for (int k = 0; k<gp.Strategies[i];k++)
                        {
                            if (gp.OptimalStrategy_R[i][j][k] == 0)
                                str += "0 ";
                            else
                                str += gp.OptimalStrategy_R[i][j][k].ToString("0.00") + ' ';
                        }
                        str += ']';
                        int sWidth = TextRenderer.MeasureText(str, dg.font).Width + 10;
                        dg_view.Rows[i].Cells[j].Value = str;
                        dg_view.Columns[j].Width = Math.Max(dg_view.Columns[j].Width, sWidth);
                    }
                }
            dg.align_and_width();
            dg.gridsize_correction();
        }

        private void CreateUtilitiesGrid(List<DataGridView> dg_view)
        {
            List<string> Names = new List<string>();
            for (int i = 0; i < Information.players_number; i++)
            {
                if (gp.ActivePlayers[i])
                {
                    if (Information.PlayersNames[i] == "")
                        Names.Add("Player " + (i + 1).ToString());
                    else
                        Names.Add(Information.PlayersNames[i]);
                }
            }

            for (int pl1 = 0; pl1 < gp.N; pl1++)
            {
                dg_view.Add(new DataGridView());
                this.Controls.Add(dg_view[pl1]);
                Graphic_Interface.Grid dg = new Graphic_Interface.Grid(dg_view[pl1], gp.Strategies[pl1], 1, "S", "T");
                dg.first_plnum = pl1;
                dg.initialize();
                dg_view[pl1].Columns[0].HeaderText = "Payoff";

                dg_view[pl1].TopLeftHeaderCell.Value = Names[pl1];

                dg.create_headers();
                if (pl1 == 0)
                    dg_view[pl1].Left = 10;
                else
                    dg_view[pl1].Left = dg_view[pl1 - 1].Right + 20;

                for (int i = 0; i < gp.Strategies[pl1]; i++)
                {
                    double u = 0;
                    for (int pl2 = 0; pl2 < gp.N; pl2++)
                    {
                        if (pl1 != pl2)
                        {
                            for (int j = 0; j < gp.Strategies[pl2]; j++)
                            {                        
                                int ii = i, jj = j;
                                if (pl1 > pl2)
                                {
                                    ii = j;
                                    jj = i;
                                }
                                u += gp.OptimalStrategy_R[pl2][pl1][j] *gp.Utility[pl1][pl2][ii][jj];
                                // * gp.OptimalStrategy_R[pl1][pl2][i]
                            }                            
                        }
                    }
                    dg_view[pl1].Rows[i].Cells[0].Value = u.ToString("0.00");
                    Graphic_Interface.Analyzer.ResizeColumn(this, false, dg_view[pl1], i, 0, u.ToString("0.00"), 100);
                }
            }
        }

        private void CreateStrategiesCombinationsGrid(DataGridView dg_view, List<DataGridView> payoffs)
        {
            this.Controls.Add(dg_view);
                        List<string> Names = new List<string>();
            for (int i = 0; i < Information.players_number; i++)
            {
                if (gp.ActivePlayers[i])
                {
                    if (Information.PlayersNames[i] == "")
                        Names.Add("Player " + (i + 1).ToString());
                    else
                        Names.Add(Information.PlayersNames[i]);
                }
            }

            Graphic_Interface.Grid dg = new Graphic_Interface.Grid(dg_view, gp.children.Count, 0, "TWDN", "T");
            dg.row_header_text = "Combination";
            dg.ComboBoxColumnsCount = gp.N;
            List<List<string>> l = new List<List<string>>();

            for (int i = 0; i < gp.N; i++)
            {
                l.Add(new List<string>());
                for (int j = 0; j<gp.Strategies[i]; j++)
                    l[i].Add(Information.strategy_letters[i] + (j + 1).ToString());
            }
            dg.combobox_items = l;
            dg.initialize();
            for (int i = 0; i < gp.N; i++)
                dg_view.Columns[i].HeaderText = Names[i];
            dg.create_headers();

            if (gp.Combinations.Count > 0)
            {
                for (int ii = 0; ii < gp.Combinations.Count; ii++)
                    for (int jj = 0; jj < gp.Combinations[ii].Count; jj++)
                    {
                        string Combination = gp.Combinations[ii][jj];
                        for (int m = 0; m < (dg_view.Rows[0].Cells[jj]
                            as DataGridViewComboBoxCell).Items.Count; m++)
                        {
                            if ((dg_view.Rows[ii].Cells[jj] as DataGridViewComboBoxCell).Items[m].ToString() == Combination)
                                dg_view.Rows[ii].Cells[jj].Value =
                                    (dg_view.Rows[ii].Cells[jj] as DataGridViewComboBoxCell).Items[m];
                        }
                    }
                dg_view.CellEndEdit += new DataGridViewCellEventHandler(DgCombinations_CellValidated);
            }
            else
            {
                for (int i = 0; i < gp.N; i++)
                {
                    double max = 0;
                    int OptStrategy = 0;
                    for (int j = 0; j < payoffs[i].RowCount; j++)
                    {
                        if (Convert.ToDouble(payoffs[i].Rows[j].Cells[0].Value) > max)
                        {
                            max = Convert.ToDouble(payoffs[i].Rows[j].Cells[0].Value);
                            OptStrategy = j;
                        }
                    }
                    dg_view.Rows[0].Cells[i].Value = (dg_view.Rows[0].Cells[i] as DataGridViewComboBoxCell).Items[OptStrategy];
                }
                dg_view.CellEndEdit += new DataGridViewCellEventHandler(DgCombinations_CellValidated);
                //dg_view.CellValidated += new DataGridViewCellEventHandler(DgCombinations_CellValidated);
            }
        }

        void DgCombinations_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!DataChanging)
                (parent as Tree).redefine_strategies(this);
        }

        public void show_result()
        {
            DataChanging = true;
            summonModelToolStripMenuItem.Text = "Edit position model";

            DgPayoffs.Rows.Clear();
            DgStrategies.Rows.Clear();
            for (int i = 0; i < DgUtilies.Count; i++)
                DgUtilies[i].Rows.Clear();
            DgCombinations.Rows.Clear();

            if (gp.N == Information.players_number)
            {
                for (int i = 0; i < gp.N; i++)
                    gp.ActivePlayers[i] = true;
            }

            create_strategies_grid(DgStrategies);
            CreateUtilitiesGrid(DgUtilies);
            CreateStrategiesCombinationsGrid(DgCombinations, DgUtilies);


            DgStrategies.Left = 10;
            DgStrategies.Top = menuStrip1.Bottom + 50;
            for (int i = 0; i < gp.N; i++)
                DgUtilies[i].Top = DgStrategies.Top + DgStrategies.Height + 60;
            int max_col = 0, max_strat = 0;
            for (int i = 0; i < gp.N; i++)
            {
                if (gp.Strategies[i] > max_strat)
                {
                    max_strat = gp.Strategies[i];
                    max_col = i;
                }
            }

            DgCombinations.Top = DgUtilies[max_col].Top + DgUtilies[max_col].Height + 60;
            DgCombinations.Left = 10;

            this.Width = Math.Max(DgCombinations.Width + 35, DgStrategies.Right + 35);
            this.Height = DgCombinations.Height + DgCombinations.Top + 80;

            this.Left = 10;
            this.Top = 10;


            StratLabel.Left = (this.Width - StratLabel.Width) / 2;
            StratLabel.Top = DgStrategies.Top - StratLabel.Height - 20;
            StratLabel.Show();

            PayoffsLabel.Left = (this.Width - PayoffsLabel.Width) / 2;
            PayoffsLabel.Top = DgUtilies[0].Top - PayoffsLabel.Height - 20;
            PayoffsLabel.Show();

            CombinationsLabel.Left = (this.Width - CombinationsLabel.Width) / 2;
            CombinationsLabel.Top = DgCombinations.Top - CombinationsLabel.Height - 20;
            CombinationsLabel.Show();

            (parent as Tree).redefine_strategies(this);
            DataChanging = false;
        }

        private void PositionInformation_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            gp.active = false;
            gp.PVF.Close();
            gp.ValuesForm_Active = false;
            gp.PWF.Close();
            gp.WeightsForm_Active = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings f = new Settings();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();

            if (gp.LHStrategy.Count > 0)
                show_result();
        }

    }
}
