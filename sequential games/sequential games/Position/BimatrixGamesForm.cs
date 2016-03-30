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
    public partial class BimatrixGamesForm : Form
    {
        List<CheckBox> ChList = new List<CheckBox>();

        Graphic_Interface.Grid strat_grid;
        Graphic_Interface.Grid Grid_A;
        Graphic_Interface.Grid Grid_B;
        public GamePosition gp;
        //static List<List<bool>> filling = new List<List<bool>>();
        string NoDataErrorMessageString;
        string PlayersKeyWordString = "Player ";

        int MinHeight_PlnumPanel = 0,
            MaxHeight_PlnumPanel = 0;

        bool AlreadyEdited = false,
            loading = false,
            manualy = false;
        static int width, height;

        public BimatrixGamesForm(GamePosition GPInput)
        {
            gp = GPInput;
            InitializeComponent();
            NoDataErrorMessageString = errormsg_strat.Text;
            n_box.Text = gp.N.ToString();

            StartingInitialization();
            CorrectStratHeaders();
            CreateStratGrid();
            CreateNavigationGrid();
            if (gp.LHStrategy.Count == 0)
            {
                if (gp.TotalDimensions < 1)
                    gp.TotalDimensions = 1;
                if (gp.Weights.Count == 0)
                {
                    gp.Weights.Add(new List<string>());
                    for (int i = 0; i < gp.N; i++)
                        gp.Weights[0].Add("1");
                }
                payoff_panel.Hide();
            }
            else
            {
                payoff_panel.Show();
                CreatePayoffGrids(0, 1, calculateValuesToolStripMenuItem.Checked);
                ModifyToolStrip();
                if (parametersToolStripMenuItem.DropDownItems.Count > 0)
                    (parametersToolStripMenuItem.DropDownItems[0] as ToolStripMenuItem).Checked = true;
            }

            LocationCorrection();
            NavigationGrid.Focus();
            CorrectNavigationGrid(false, true);
        }

        private void StartingInitialization()
        {
            for (int i = 0; i < gp.N; i++)
                gp.Strategies.Add(0);

            Payoffs_OuterCorrection(0, gp.N, gp.N);
        }

        private void LocationCorrection()
        {
            manualy = true;
            width = this.Width;
            height = this.Height;
            
            strat_panel.Top = plnum_panel.Bottom + 6;
            strat_panel.Height = strat_grid_view.Bottom + 10;

            if ((payoff_panel.Visible) || (gp.LHStrategy.Count > 0))
            {
                if (A.Width > 500)
                {
                    A.Width = 500;
                    A.Height += 17;
                }
                if (B.Width > 500)
                {
                    B.Width = 500;
                    B.Height += 17;
                }

                payoff_panel.Top = strat_panel.Bottom + 4;
                payoff_panel.Height = A.Bottom + 10;
                navigation_panel.Top = payoff_panel.Bottom + 6;
                B.Left = A.Right + 20;
                pl1_lb.Left = A.Left + (A.Width - pl1_lb.Width) / 2;
                pl2_lb.Left = B.Left + (B.Width - pl2_lb.Width) / 2;
                vs_lb.Left = A.Left + (A.Width + B.Left - vs_lb.Width) / 2;
            }
            else
                navigation_panel.Top = strat_panel.Bottom + 6;

            this.Height = navigation_panel.Bottom + menuStrip1.Height + 19;
            this.Width = Math.Max(strat_grid_view.Right, NavigationGrid.Right) + 42;
            if ((A.Visible) || (gp.LHStrategy.Count > 0))
                this.Width = Math.Max(B.Right + 38, this.Width);

            if (this.Width > 1080)
                this.Width = 1080;

            done_btn.Top = navigation_panel.Height - done_btn.Height - 30;
            done_btn.Left = navigation_panel.Right - done_btn.Width - 30;
            n_box.Left = (this.Width - n_box.Width - 6 - n_label.Width) / 2;
            n_label.Left = n_box.Left + n_box.Width + 6;
            if (gp.PVF != null)
            {
                gp.PVF.Left = this.Right;
                gp.PWF.Left = this.Right;
                gp.PVF.Top = this.Top;
                gp.PWF.Top = gp.PVF.Bottom;
            }
            this.Focus();
            manualy = false;
        }

        private void CreateStratGrid()
        {
            strat_grid_view.Left = 10;
            strat_grid_view.Top = 10;
            strat_grid = new Graphic_Interface.Grid(strat_grid_view, 1, gp.N, "T", "T");
            strat_grid.initialize();
            strat_grid_view.Rows[0].HeaderCell.Value = "Number of Strategies";
            int k = 0;
            for (int i = 0; i < Information.players_number; i++)
            {
                if (gp.N == Information.players_number)
                {
                    if (Information.PlayersNames.Count > 0)
                    {
                        if ((Information.PlayersNames[i] == null) || (Information.PlayersNames[i] == ""))
                            strat_grid_view.Columns[i].HeaderText = "Player " + (i + 1).ToString();
                        else
                            strat_grid_view.Columns[i].HeaderText = Information.PlayersNames[i].ToString();
                    }
                }
                else
                {
                    if (ChList.Count != 0)
                    {
                        if (ChList[i].Checked)
                        {
                            if ((Information.PlayersNames[i] == null) || (Information.PlayersNames[i] == ""))
                                strat_grid_view.Columns[k++].HeaderText = "Player " + (i + 1).ToString();
                            else
                                strat_grid_view.Columns[k++].HeaderText = Information.PlayersNames[i].ToString();
                        }
                    }
                }
            }
            strat_grid.create_headers();


            for (int i = 0; i < gp.N; i++)
            {
                if (gp.Strategies.Count > i)
                {
                    if (gp.Strategies[i] != 0)
                        strat_grid_view.Rows[0].Cells[i].Value = gp.Strategies[i];
                    else
                        strat_grid_view.Rows[0].Cells[i].Value = null;
                }
            }
            strat_grid_view.TopLeftHeaderCell.Value = "Player";
        }

        private void CreatePayoffGrids(int pl1, int pl2, bool numerical)
        {
            payoff_panel.Show();
            int str1 = Convert.ToInt32(strat_grid_view.Rows[0].Cells[pl1].Value.ToString());
            int str2 = Convert.ToInt32(strat_grid_view.Rows[0].Cells[pl2].Value.ToString());

            Grid_A = new Graphic_Interface.Grid(A, str1, str2, "S", "S");
            Grid_B = new Graphic_Interface.Grid(B, str1, str2, "S", "S");
            Grid_A.gridsize = 1;
            Grid_B.gridsize = 1;
            Grid_A.initialize();
            Grid_B.initialize();
            Grid_A.create_headers();
            Grid_B.create_headers();

            for (int i = 0; i < A.RowCount; i++)
                for (int j = 0; j < A.Rows[i].Cells.Count; j++)
                {
                    if (pl1 > pl2)
                    {
                        if (numerical)
                        {
                            A[j,i].Value = gp.NumericalPayoffs[gp.CurrentDimension][pl1][pl2][j][i];
                            B[j,i].Value = gp.NumericalPayoffs[gp.CurrentDimension][pl2][pl1][j][i];
                        }
                        else
                        {
                            A[j,i].Value = gp.payoffs[gp.CurrentDimension].Array[pl1][pl2][j][i];
                            B[j,i].Value = gp.payoffs[gp.CurrentDimension].Array[pl2][pl1][j][i];
                        }
                    }
                    else
                    {
                        if (i < gp.payoffs[gp.CurrentDimension].Array[pl1][pl2].Count)
                            if (j < gp.payoffs[gp.CurrentDimension].Array[pl1][pl2][i].Count)
                            {
                                if (numerical)
                                {
                                    A[j,i].Value = gp.NumericalPayoffs[gp.CurrentDimension][pl1][pl2][i][j];
                                    B[j,i].Value = gp.NumericalPayoffs[gp.CurrentDimension][pl2][pl1][i][j];
                                }
                                else
                                {
                                    A[j,i].Value = gp.payoffs[gp.CurrentDimension].Array[pl1][pl2][i][j];
                                    B[j,i].Value = gp.payoffs[gp.CurrentDimension].Array[pl2][pl1][i][j];
                                }
                            }
                    }
                    Graphic_Interface.Analyzer.ResizeColumn(this, false, A, i, j,
                        A[j,i].Value.ToString(), 100);
                    Graphic_Interface.Analyzer.ResizeColumn(this, false, B, i, j,
                        B[j,i].Value.ToString(), 100);
                    if (A[j,i].Value.ToString() == "")                    
                        A[j,i].Value = "0";
                    if (B[j,i].Value.ToString() == "")
                        B[j,i].Value = "0";

                }

            A.Left = 10;
            A.Top = pl1_lb.Top + pl1_lb.Height + 10;
            B.Top = A.Top;
            A.Tag = pl1;
            B.Tag = pl2;
            B.Left = A.Left + A.Width + 50;
            if (Information.PlayersNames[pl1] == "")
                pl1_lb.Text = PlayersKeyWordString + (pl1 + 1).ToString();
            else
                pl1_lb.Text = Information.PlayersNames[pl1];
            if (Information.PlayersNames[pl2] == "")
                pl2_lb.Text = PlayersKeyWordString + (pl2 + 1).ToString();
            else
                pl2_lb.Text = Information.PlayersNames[pl2];
            pl1_lb.Left = A.Left + (A.Width - pl1_lb.Width) / 2;
            pl2_lb.Left = B.Left + (B.Width - pl2_lb.Width) / 2;
            vs_lb.Left = A.Left + A.Width + 10;
        }

        private void CreateNavigationGrid()
        {
            NavigationGrid.Rows.Clear();

            Graphic_Interface.Grid grid = new Graphic_Interface.Grid(NavigationGrid, gp.N, gp.N, "N", "N");
            grid.initialize();
            grid.create_headers();
            NavigationGrid.TopLeftHeaderCell.Value = "Player";

            for (int i = 0; i < gp.N; i++)
            {
                NavigationGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
                NavigationGrid.Rows[i].Cells[i].Style.SelectionBackColor = Color.SaddleBrown;
                NavigationGrid.Rows[i].Cells[i].Style.BackColor = Color.SaddleBrown;
            }

            Hint.Left = NavigationGrid.Left;
            Hint.Top = NavigationGrid.Bottom + 10;

            navigation_panel.Height = Hint.Bottom + 20;
        }

        private void CorrectNavigationGrid(bool CorrectSize, bool CorrectData)
        {
            if (CorrectSize)
                CreateNavigationGrid();

            if (CorrectData)
            {
                int ActivePlayer1 = 0,
                    ActivePlayer2 = 0;

                if (A.Visible)
                {
                    ActivePlayer1 = (int)A.Tag;
                    ActivePlayer2 = (int)B.Tag;

                    bool Filled = false;
                    for (int i = 0; i < A.Rows.Count; i++)
                    {
                        if (Filled)
                            break;
                        for (int j = 0; j < A.Rows[i].Cells.Count; j++)
                        {
                            if (A[j,i].Value != null)
                            if ((A[j,i].Value.ToString() != "")&(A[j,i].Value.ToString() != "0"))
                                {
                                    Filled = true;
                                    break;
                                }

                            if (B[j, i].Value != null)
                                if ((B[j, i].Value.ToString() != "")&&(B[j,i].Value.ToString() != "0"))
                                {
                                    Filled = true;
                                    break;
                                }
                        }
                    }
                    if (Filled)
                    {
                        NavigationGrid.Rows[ActivePlayer1].Cells[ActivePlayer2].Value = "+";
                        NavigationGrid.Rows[ActivePlayer2].Cells[ActivePlayer1].Value = "+";
                    }
                    else
                    {
                        NavigationGrid.Rows[ActivePlayer1].Cells[ActivePlayer2].Value = null;
                        NavigationGrid.Rows[ActivePlayer2].Cells[ActivePlayer1].Value = null;
                    }
                }

                for (int i = 0; i < gp.N; i++)
                    for (int j = 0; j < gp.N; j++)
                    {
                        if ((i<j)&&((i!=ActivePlayer1)||(j!=ActivePlayer2))&&((i!=ActivePlayer2)||(j!=ActivePlayer1)))
                        {
                            bool filled = false;
                            if ((gp.Strategies[i] != 0) && (gp.Strategies[j] != 0))
                            {
                                for (int ii = 0; ii < gp.Strategies[i]; ii++)
                                    for (int jj = 0; jj < gp.Strategies[j]; jj++)
                                    {                                        
                                        if (gp.payoffs[0].Array[i][j][ii][jj] != "")
                                            filled = true;
                                        if (gp.payoffs[0].Array[j][i][ii][jj] != "")
                                            filled = true;
                                    }
                            }

                            if (filled)
                            {
                                NavigationGrid.Rows[i].Cells[j].Value = "+";
                                NavigationGrid.Rows[j].Cells[i].Value = "+";
                            }
                            else
                            {
                                NavigationGrid.Rows[i].Cells[j].Value = null;
                                NavigationGrid.Rows[j].Cells[i].Value = null;
                            }
                        }
                    }
                bool complete = true;
                for (int i = 0; i<gp.N; i++)
                    for (int j = 0; j < gp.N; j++)
                    {
                        if (i > j)
                            if (NavigationGrid.Rows[i].Cells[j].Value == null)
                                complete = false;
                    }

                if (complete)
                {
                    done_btn.Text = "Done";
                    done_btn.Width = 120;
                    done_btn.Height = 30;
                    done_btn.ForeColor = Color.White;
                    done_btn.Left = navigation_panel.Right - done_btn.Width - 30;
                    //done_btn.Top = Hint.Top + 50;
                    done_btn.Enabled = true;
                }
                else
                {
                    done_btn.Text = "Some matrixes are empty";
                    done_btn.Width = 160;
                    done_btn.Height = 60;
                    done_btn.ForeColor = Color.DarkGray;
                    done_btn.Left = navigation_panel.Right - done_btn.Width - 30;
                    //done_btn.Top = Hint.Top + 50;
                    done_btn.Enabled = false;
                }
            }
        }

        private void save_payoff_matrixes_data(int pl1, int pl2, int str1, int str2)
        {
            if (gp.CurrentDimension != -1)
            {
                gp.payoffs[gp.CurrentDimension].Array[pl1][pl2].Clear();
                gp.payoffs[gp.CurrentDimension].Array[pl2][pl1].Clear();

                int n, m;
                if (pl1 > pl2)
                {
                    n = str2;
                    m = str1;
                }
                else
                {
                    n = str1;
                    m = str2;
                }

                for (int i = 0; i < n; i++)
                {
                    gp.payoffs[gp.CurrentDimension].Array[pl1][pl2].Add(new List<string>());
                    gp.payoffs[gp.CurrentDimension].Array[pl2][pl1].Add(new List<string>());
                    for (int j = 0; j < m; j++)
                    {
                        gp.payoffs[gp.CurrentDimension].Array[pl1][pl2][i].Add("");
                        gp.payoffs[gp.CurrentDimension].Array[pl2][pl1][i].Add("");
                    }
                }

                for (int i = 0; i < str1; i++)
                    for (int j = 0; j < str2; j++)
                    {
                        int ii = i, jj = j;

                        if (pl1 > pl2)
                        {
                            jj = i;
                            ii = j;
                        }
                        if ((A[jj, ii].Value == null) || (A[jj, ii].Value.ToString() == "0") || (A[jj, ii].Value.ToString() == ""))
                        {
                            gp.payoffs[gp.CurrentDimension].Array[pl1][pl2][ii][jj] = "";
                            gp.payoffs[gp.CurrentDimension].Array[pl2][pl1][ii][jj] = "";
                        }
                        else
                        {
                            gp.payoffs[gp.CurrentDimension].Array[pl1][pl2][ii][jj] = A[j, i].Value.ToString();
                            gp.payoffs[gp.CurrentDimension].Array[pl2][pl1][ii][jj] = B[j, i].Value.ToString();
                        }
                    }
            }
        }
    
        public void navigation_grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (A.Visible)
                save_payoff_matrixes_data((int)A.Tag, (int)B.Tag, gp.Strategies[(int)A.Tag], gp.Strategies[(int)B.Tag]);

            int pl1 = e.RowIndex, pl2 = e.ColumnIndex;
            if ((strat_grid_view.Rows[0].Cells[pl1].Value != null) && (strat_grid_view.Rows[0].Cells[pl2].Value != null))
            {
                errormsg_strat.Hide();
                if (pl1 != pl2)
                {
                    if (!A.Visible)
                    {
                        //modelAsTreeToolStripMenuItem.Visible = true;
                        payoff_panel.Show();
                    }

                    CreatePayoffGrids(pl1, pl2,calculateValuesToolStripMenuItem.Checked);
                    CorrectNavigationGrid(false, true);
                }
                LocationCorrection();
            }
            else
            {
                errormsg_strat.Show();
                int empty1 = -1, empty2 = -1;
                if (strat_grid_view.Rows[0].Cells[pl1].Value == null)
                    empty1 = pl1 + 1;
                if (strat_grid_view.Rows[0].Cells[pl2].Value == null)
                    empty2 = pl2 + 1;
                errormsg_strat.Show();
                if (empty1 != -1)
                {
                    if (empty2 != -1)
                        errormsg_strat.Text = NoDataErrorMessageString + "s " + empty1.ToString() + " and " + empty2.ToString();
                    else
                        errormsg_strat.Text = NoDataErrorMessageString + " " + empty1.ToString();
                }
                if ((empty2!=-1)&&(empty1 == -1))
                    errormsg_strat.Text = NoDataErrorMessageString + " " + empty2.ToString();

                errormsg_strat.Left = NavigationGrid.Left + NavigationGrid.Width + 50;
                LocationCorrection();
                payoff_panel.Hide();
            }
        }

        private void n_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;            
        }

        private void strategies_correction()
        {
            gp.Strategies.Clear();
                for (int i = 0; i < gp.N; i++)
                {
                    if (i < strat_grid_view.Rows[0].Cells.Count)
                    {
                        object cell_value = strat_grid_view.Rows[0].Cells[i].Value;

                        if (cell_value != null)
                        {
                            string check_result = Graphic_Interface.Analyzer.
                            CheckValidStringInt(cell_value.ToString(), 1, 50,true);
                            int str = Convert.ToInt32(check_result);
                            gp.Strategies.Add(str);
                        }
                        else
                            gp.Strategies.Add(0);
                    }
                    else
                        gp.Strategies.Add(0);
                }
            CreateStratGrid();
        }

        private void Payoffs_OuterCorrection(int OldPlayersNewPlayersNumberumber, int NewPlayersNumber, int diff)
        {
            int min = Math.Min(OldPlayersNewPlayersNumberumber, NewPlayersNumber),
                max = Math.Max(OldPlayersNewPlayersNumberumber, NewPlayersNumber);

            //Добавление игр для старых игроков с новыми
            for (int i = 0; i < min; i++)
            {
                if (diff > 0)
                {
                    for (int j = min; j < max; j++)
                    {
                        for (int d = 0; d < gp.TotalDimensions; d++)
                            gp.payoffs[d].Array[i].Add(new List<List<string>>());
                    }
                }
                else
                {
                    for (int d = 0; d < gp.TotalDimensions; d++)
                        gp.payoffs[d].Array[i].RemoveRange(min, -diff);
                }
            }

            //Добавление игр для новых игроков со всеми
            for (int d = 0; d < gp.TotalDimensions; d++)
            {
                if (diff > 0)
                {
                    for (int i = min; i < max; i++)
                    {

                        gp.payoffs[d].Array.Add(new List<List<List<string>>>());
                        for (int j = 0; j < NewPlayersNumber; j++)
                            gp.payoffs[d].Array[i].Add(new List<List<string>>());
                    }
                }
                else
                    gp.payoffs[d].Array.RemoveRange(min, -diff);
            }
        }

        private void Payoffs_InnerCorrection(int OldStrategy, int NewStrategy, int ActivePlayer)
        {
            for (int d = 0; d < gp.TotalDimensions; d++)
            {
                int diff = NewStrategy - OldStrategy;
                if (OldStrategy == 0)
                {
                    for (int i = 0; i < gp.N; i++)
                    {
                        if (i < ActivePlayer)
                        {
                            for (int p = 0; p < gp.Strategies[i]; p++)
                            {
                                if (gp.payoffs[d].Array[i][ActivePlayer].Count <= p)
                                {
                                    gp.payoffs[d].Array[i][ActivePlayer].Add(new List<string>());
                                    gp.payoffs[d].Array[ActivePlayer][i].Add(new List<string>());
                                }
                                for (int q = 0; q < diff; q++)
                                {
                                    gp.payoffs[d].Array[i][ActivePlayer][p].Add("");
                                    gp.payoffs[d].Array[ActivePlayer][i][p].Add("");
                                }
                            }
                        }
                        if (i > ActivePlayer)
                        {
                            for (int p = 0; p < diff; p++)
                            {
                                gp.payoffs[d].Array[i][ActivePlayer].Add(new List<string>());
                                gp.payoffs[d].Array[ActivePlayer][i].Add(new List<string>());
                                for (int q = 0; q < gp.Strategies[i]; q++)
                                {
                                    gp.payoffs[d].Array[i][ActivePlayer][OldStrategy + p].Add("");
                                    gp.payoffs[d].Array[ActivePlayer][i][OldStrategy + p].Add("");
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < gp.N; i++)
                    {
                        if ((i < ActivePlayer) && (gp.Strategies[i] != 0))
                        {
                            if (diff > 0)
                            {
                                for (int p = 0; p < gp.Strategies[i]; p++)
                                {
                                    for (int q = 0; q < diff; q++)
                                    {
                                        gp.payoffs[d].Array[i][ActivePlayer][p].Add("");
                                        gp.payoffs[d].Array[ActivePlayer][i][p].Add("");
                                    }
                                }
                            }
                            else
                            {
                                for (int p = 0; p < gp.Strategies[i]; p++)
                                {
                                    gp.payoffs[d].Array[i][ActivePlayer][p].RemoveRange(NewStrategy, -diff);
                                    gp.payoffs[d].Array[ActivePlayer][i][p].RemoveRange(NewStrategy, -diff);
                                }
                            }
                        }
                        if ((i > ActivePlayer) && (gp.Strategies[i] != 0))
                        {
                            if (diff > 0)
                            {
                                for (int p = 0; p < diff; p++)
                                {
                                    gp.payoffs[d].Array[i][ActivePlayer].Add(new List<string>());
                                    gp.payoffs[d].Array[ActivePlayer][i].Add(new List<string>());
                                    for (int q = 0; q < gp.Strategies[i]; q++)
                                    {
                                        gp.payoffs[d].Array[i][ActivePlayer][p + OldStrategy].Add("");
                                        gp.payoffs[d].Array[ActivePlayer][i][p + OldStrategy].Add("");
                                    }
                                }
                            }
                            else
                            {
                                gp.payoffs[d].Array[i][ActivePlayer].RemoveRange(NewStrategy, -diff);
                                gp.payoffs[d].Array[ActivePlayer][i].RemoveRange(NewStrategy, -diff);
                            }
                        }
                    }
                }
            }
        }

        private void n_box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string check_result = Graphic_Interface.Analyzer.CheckValidStringInt(n_box.Text, 2, 99,true);
                if (check_result != "")
                {                    
                    int old_N = gp.N,
                        N = Convert.ToInt32(check_result),
                        diff = N - old_N;

                    if (N > Information.players_number)
                    {
                        System.Windows.Forms.MessageBox.Show("Players number cannot exceed tree player number");
                        n_box.Text = Information.players_number.ToString();
                    }
                    else
                    {
                        if (diff != 0)
                        {
                            gp.N = N;
                            CorrectStratHeaders();
                            strategies_correction();
                            CorrectNavigationGrid(true, false);
                            Payoffs_OuterCorrection(old_N, N, diff);
                            AlreadyEdited = true;
                            strat_panel.Focus();
                            AlreadyEdited = false;

                            if (gp.N < N)
                            {
                                for (int i = gp.N; i < N; i++)
                                    gp.Strategies.Add(0);
                            }
                            LocationCorrection();
                        }
                    }
                }
            }            
        }

        private void CorrectStratHeaders()
        {
            if (gp.N != Information.players_number)
            {
                if (MinHeight_PlnumPanel == 0)
                    MinHeight_PlnumPanel = plnum_panel.Height;
                if (MaxHeight_PlnumPanel != 0)
                {
                    plnum_panel.Height = MaxHeight_PlnumPanel;
                    for (int i = 0; i < gp.N; i++)
                        ChList[i].Checked = true;
                    for (int i = gp.N; i < Information.players_number; i++)
                        ChList[i].Checked = false;
                }
                else
                {
                    for (int i = 0; i < Information.players_number; i++)
                    {
                        Label l = new Label();
                        if ((Information.PlayersNames[i] == null) || (Information.PlayersNames[i] == ""))
                            l.Text = "Player " + (i + 1).ToString();
                        else
                            l.Text = Information.PlayersNames[i].ToString();

                        l.Font = new System.Drawing.Font("Bookman Old Style", 12);
                        l.Size = TextRenderer.MeasureText(l.Text, l.Font);
                        l.Left = (this.Width - l.Width) / 2;
                        l.Top = i * 30 + n_box.Bottom + 20;

                        plnum_panel.Controls.Add(l);

                        CheckBox c = new CheckBox();
                        c.Left = l.Right + 40;
                        c.Top = l.Top;
                        c.Size = new System.Drawing.Size(30, 30);
                        if (i < gp.N - 1)
                            c.Checked = true;

                        c.CheckedChanged += new EventHandler(CheckBoxChecked);
                        plnum_panel.Controls.Add(c);
                        ChList.Add(c);
                    }

                    Label L = new Label();
                    L.Text = "Choose \nactive \nplayers";
                    L.Font = new System.Drawing.Font("Bookman Old Style", 16);
                    L.Size = TextRenderer.MeasureText(L.Text, L.Font);
                    L.Left = 30;
                    L.Top = (plnum_panel.Controls[plnum_panel.Controls.Count - 2 * Information.players_number + 1].Top +
                    plnum_panel.Controls[plnum_panel.Controls.Count - Information.players_number].Top) / 2 - 15;
                    plnum_panel.Controls.Add(L);

                    MaxHeight_PlnumPanel = plnum_panel.Controls[plnum_panel.Controls.Count - 2].Bottom + 20;
                    plnum_panel.Height = MaxHeight_PlnumPanel;
                    LocationCorrection();

                    ChList[gp.N - 1].Checked = true;
                }
            }
            else
            {
                if (MinHeight_PlnumPanel != 0)
                    plnum_panel.Height = MinHeight_PlnumPanel;
            }
        }

        void CheckBoxChecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                int CheckedCount = 0;
                for (int i = 0; i < Information.players_number; i++)
                {
                    if (ChList[i].Checked)
                        CheckedCount++;
                }
                if (CheckedCount > gp.N)
                    (sender as CheckBox).Checked = false;
            }

            if ((sender as CheckBox).Checked)
            {
                CreateStratGrid();
                if (payoff_panel.Visible)
                    CreatePayoffGrids((int)A.Tag, (int)B.Tag, false);
            }

            if (gp.ActivePlayers.Count < Information.players_number)
            {
                int APC = gp.ActivePlayers.Count;
                for (int i = APC; i<Information.players_number; i++)
                    gp.ActivePlayers.Add(false);
            }
            for (int i = 0; i < ChList.Count; i++)
            {
                if (ChList[i].Checked)
                    gp.ActivePlayers[i] = true;
                else
                    gp.ActivePlayers[i] = false;
            }
        }

        private void BimatrixGamesForm_Resize(object sender, EventArgs e)
        {
            plnum_panel.Width += (this.Width - width);
            strat_panel.Width += (this.Width - width);
            payoff_panel.Width += (this.Width - width);
            navigation_panel.Width += (this.Width - width);
            if (!manualy)
                navigation_panel.Height += (this.Height - height);
            width = this.Width;
            height = this.Height;
        }

        public void done_btn_Click(object sender, EventArgs e)
        {
            if (gp.ActivePlayers.Count < gp.N)
            {
                gp.ActivePlayers.Clear();
                if (Information.players_number == gp.N)
                {
                    for (int i = 0; i < gp.N; i++)
                        gp.ActivePlayers.Add(true);
                }
                else
                {
                    for (int i = 0; i < ChList.Count; i++)
                        gp.ActivePlayers.Add(ChList[i].Checked);
                }
            }

            save_payoff_matrixes_data((int)A.Tag, (int)B.Tag, gp.Strategies[(int)A.Tag], gp.Strategies[(int)B.Tag]);
            (this.Tag as PositionInformation_Form).SolveGame(gp);
            this.Close();
        }

        private void Payoff_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            object cell_value = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (cell_value != null)
            {
                //string cell_string = cell_value.ToString(),
                //    check_result = Graphic_Interface.Analyzer.CheckValidStringDouble(cell_string, 0, 0, true);
                //if (check_result != "")
                //{
                    CorrectNavigationGrid(false, true);
                    Graphic_Interface.Analyzer.ResizeColumn(this, false, (sender as DataGridView),
                        e.RowIndex, e.ColumnIndex, cell_value.ToString(), 100);
                    LocationCorrection();
                //}
                //else
                //    (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            }            
        }

        public void strat_grid_view_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!loading)
            {
                int pl = e.ColumnIndex;
                object cell_value = strat_grid_view.Rows[0].Cells[pl].Value;

                if (cell_value != null)
                {
                    string check_result = Graphic_Interface.Analyzer.CheckValidStringInt(cell_value.ToString(), 1, 99,true);
                    if (check_result != "")
                    {
                        int OldStrategy = gp.Strategies[pl],
                            str = Convert.ToInt32(check_result),
                            diff = str - OldStrategy,
                            pl1 = -1,
                            pl2 = -1;

                        if (diff != 0)
                        {
                            if (A.Visible)
                            {
                                pl1 = (int)A.Tag;
                                pl2 = (int)B.Tag;
                            }
                            Payoffs_InnerCorrection(OldStrategy, str, pl);

                            if (((pl1 == pl) || (pl2 == pl))&&(pl1!=-1))
                                CreatePayoffGrids(pl1, pl2,calculateValuesToolStripMenuItem.Checked);

                            gp.Strategies[pl] = Convert.ToInt32(check_result);

                            LocationCorrection();
                        }
                    }
                }
            }
        }


        private void open_menu_Click(object sender, EventArgs e)
        {
            bool manualy = true;
            if (sender is BimatrixGamesForm)
                manualy = false;
            Graphic_Interface.PositionFileOpening Data = new Graphic_Interface.PositionFileOpening(openFileDialog1);

            if (Data.OpenPositionFile(false, "", manualy))
            {
                gp.N = Data.gp.N;
                gp.Strategies = Data.gp.Strategies;
                if (gp.parent == null)
                {
                    gp.AdParamValues = Data.gp.AdParamValues;
                    gp.Weights = Data.gp.Weights;
                    gp.TotalDimensions = Data.gp.TotalDimensions;
                }
                gp.payoffs = Data.gp.payoffs;
                gp.SaveFilePath = Data.gp.SaveFilePath;


                gp.CurrentDimension = 0;
                parametersToolStripMenuItem.DropDownItems.Clear();
                
                n_box.Text = gp.N.ToString();
                
                List<int> test = gp.Strategies;
                loading = true;
                CreateStratGrid();
                CorrectStratHeaders();
                payoff_panel.Show();
                CreatePayoffGrids(0, 1,calculateValuesToolStripMenuItem.Checked);
                CreateNavigationGrid();
                LocationCorrection();
                loading = false;
                CorrectNavigationGrid(false, true);
                this.CenterToScreen();
                if (gp.AdParamValues.Count > 0)
                {
                    viewToolStripMenuItem_Click(this, new EventArgs());
                    (parametersToolStripMenuItem.DropDownItems[0] as ToolStripMenuItem).Checked = true;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            gp.N = 2;
            gp.Strategies.Clear();
            gp.payoffs[0].Array.Clear();
            gp.payoffs.RemoveRange(1, gp.payoffs.Count - 1);
            gp.TotalDimensions = 1;
            gp.CurrentDimension = 0;
            if (gp.PVF != null)
            {
                gp.PVF.Close();
                gp.PWF.Close();
            }

            n_box.Text = gp.N.ToString();
            StartingInitialization();
            //CorrectStratHeaders();
            CreateStratGrid();
            CreateNavigationGrid();

            if (gp.LHStrategy.Count == 0)
                payoff_panel.Hide();
            else
                CreatePayoffGrids(0, 1,calculateValuesToolStripMenuItem.Checked);

            LocationCorrection();


            NavigationGrid.Focus();
            CorrectNavigationGrid(false, true);
        }

        private void n_box_Leave(object sender, EventArgs e)
        {
            if (!AlreadyEdited)
                n_box_KeyDown(this, new KeyEventArgs(Keys.Enter));
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphic_Interface.PositionFileWriting PFW = new Graphic_Interface.PositionFileWriting(saveFileDialog1,gp);
            PFW.WritePositionFile(true, "");
        }

        public void modelAsTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (A.Visible)
            {
                List<string> PlayersNames = new List<string>();
                if (gp.N == Information.players_number)
                    PlayersNames = Information.PlayersNames;
                else
                {
                    for (int i = 0; i < ChList.Count; i++)
                    {
                        if (ChList[i].Checked)
                        {
                            if (Information.PlayersNames[i] == "")
                                PlayersNames.Add("Player " + (i + 1).ToString());
                            else
                                PlayersNames.Add(Information.PlayersNames[i]);
                        }
                    }
                }

                if (PlayersNames.Count > 0)
                {
//Debug//
                    ExtensiveFormGame SG = new ExtensiveFormGame(this, pl1_lb.Text, pl2_lb.Text);
                    DebugClass.EFG = SG;
                    SG.StartPosition = FormStartPosition.CenterScreen;
                    SG.Show();
                }
                else
                    System.Windows.Forms.MessageBox.Show("You have to select at least one player");
            }
            else
                System.Windows.Forms.MessageBox.Show("Click on the navigation panel to revel competiting players");
        }

        private List<string> CalculateLeafPosition(TreePosition TP)
        {
            List<string> result = new List<string>();
            result.Add("");
            result.Add("");
            string s = "";

            while (TP.parent != null)
            {
                TreePosition Papa = TP.parent;
                for (int i = 0; i<Papa.children.Count; i++)
                {
                    if (Papa.children[i] == TP)
                    {
                        s += i.ToString();
                    }
                }
                TP = Papa;
            }
            for (int i = 0; i < s.Length; i++)
                result[i % 2] += s[s.Length - i - 1];
            return result;
        }

        private List<string> GetTitles(List<int> Division, int n)
        {
            List<string> Result = new List<string>();
            for (int i = 0; i < n; i++)
            {
                int div = 1;
                for (int j = 1; j < Division.Count; j++)
                    div *= Division[j];
                string s = (i / div).ToString();

                for (int j = 1; j < Division.Count; j++)
                {
                    div = 1;
                    for (int k = j + 1; k < Division.Count; k++)
                        div *= Division[k];
                    s += ((i / div) % Division[j]).ToString();
                }
                Result.Add(s);
            }
            return Result;
        }

        private void FillMatrixByEFGData(List<TreePosition> Tree, List<int> AlternativesCount)
        {
                        int pl1 = 0,//(int)A.Tag,
                pl2 = 1,//(int)B.Tag,
                s1 = 1, s2 = 1;
            List<int> RowDivision = new List<int>();
            List<int> ColumnDivision = new List<int>();
            
            for (int i = 0; i < AlternativesCount.Count; i++)
            {
                if (i % 2 == 0)
                {
                    s1 *= AlternativesCount[i];
                    RowDivision.Add(AlternativesCount[i]);
                }
                else
                {
                    s2 *= AlternativesCount[i];
                    ColumnDivision.Add(AlternativesCount[i]);
                }
            }

            List<string> Rows = GetTitles(RowDivision, s1);
            List<string> Columns = GetTitles(ColumnDivision, s2);

            strat_grid_view[pl1, 0].Value = s1;
            strat_grid_view[pl2, 0].Value = s2;

            Information.PlayersNames.Add("Swlabr");
            Information.PlayersNames.Add("Petrovich");
            CreatePayoffGrids(pl1, pl2, calculateValuesToolStripMenuItem.Checked);
            for (int i = 0; i<A.Rows.Count; i++)
                for (int j = 0; j < A.Columns.Count; j++)
                {
                    A[j, i].Value = 0;
                    B[j, i].Value = 0;
                }

            for (int i = 0; i < Tree.Count; i++)
            {
                if (Tree[i].prize != "")
                {
                    List<string> LP = CalculateLeafPosition(Tree[i]);
                    string Row = LP[0],
                        Column = LP[1];
                    bool RowFound = false,
                        ColumnFound = false;

                    int RowStartIndex = -1,
                        RowEndIndex = -1,
                        ColumnStartIndex = -1,
                        ColumnEndIndex = -1;

                    for (int j = 0; j < Rows.Count; j++)
                    {
                        if (Row == Rows[j])
                        {
                            RowFound = true;
                            RowStartIndex = j;
                            RowEndIndex = j;
                            break;
                        }
                    }
                    if ((!RowFound) && (Row.Length < Rows[0].Length))
                    {
                        for (int j = 0; j < Rows.Count; j++)
                        {
                            string Cut = Rows[j].Substring(0, Row.Length);
                            if (Cut == Row)
                            {
                                if (RowStartIndex == -1)
                                    RowStartIndex = j;
                                else
                                {
                                    RowEndIndex = j;
                                    break;
                                }
                            }
                        }
                    }

                    for (int j = 0; j < Columns.Count; j++)
                    {
                        if (Column == Columns[j])
                        {
                            ColumnFound = true;
                            ColumnStartIndex = j;
                            ColumnEndIndex = j;
                            break;
                        }
                    }
                    if ((!ColumnFound) && (Column.Length < Columns[0].Length))
                    {
                        for (int j = 0; j < Columns.Count; j++)
                        {
                            string Cut = Columns[j].Substring(0, Column.Length);
                            if (Cut == Column)
                            {
                                if (ColumnStartIndex == -1)
                                    ColumnStartIndex = j;
                                else
                                {
                                    ColumnEndIndex = j;
                                    break;
                                }
                            }
                        }
                    }

                    if (RowStartIndex == RowEndIndex)
                    {
                        if (ColumnStartIndex == ColumnEndIndex)
                            A[ColumnStartIndex,RowStartIndex].Value = Tree[i].prize;
                        else
                        {
                            for (int j = ColumnStartIndex; j <= ColumnEndIndex; j++)
                                A[j,RowStartIndex].Value = Tree[i].prize;
                        }
                    }
                    else
                    {
                        if (ColumnStartIndex == ColumnEndIndex)
                        {
                            for (int j = RowStartIndex; j <= RowEndIndex; j++)
                                A[ColumnEndIndex, j].Value = Tree[i].prize;
                        }
                        else
                        {
                            for (int p = RowStartIndex; p <= RowEndIndex; p++)
                                for (int q = ColumnStartIndex; q <= ColumnEndIndex; q++)
                                    A[q, p].Value = Tree[i].prize;
                        }
                    }
                }
        }
        }

        public void ModelViaExtensiveFormGame(List<TreePosition> Tree, List<int> AlternativesCount)
        {
            FillMatrixByEFGData(Tree,AlternativesCount);

            for (int i = 0; i<A.Rows.Count; i++)
                for (int j = 0; j < A.Columns.Count; j++)
                {
                    if (TextRenderer.MeasureText(A[j, i].Value.ToString(), A.Font).Width > A.Columns[j].Width)
                    {
                        int diff = TextRenderer.MeasureText(A[j,i].Value.ToString(), A.Font).Width - A.Columns[j].Width;
                        A.Columns[j].Width += diff;
                        A.Width += diff;
                    }
                    B[j, i].Value = "-" + A[j, i].Value.ToString();
                }             
            
        }
//Debug//
        public void setToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParametersSettingsForm PSF = new ParametersSettingsForm();
//Debug//
            DebugClass.PSF = PSF;
            PSF.StartPosition = FormStartPosition.CenterScreen;
            if (sender is BimatrixGamesForm)
                PSF.Show();
            else
            {
                if (PSF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    viewToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        //public
        public void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Information.AP_Names.Count > 1)
            {
                gp.ShowParametersForms(this);
                ModifyToolStrip();
            }
            else
                System.Windows.Forms.MessageBox.Show("No additional parameters are set");
        }

        private void ModifyToolStrip()
        {
            bool FirstTimeCreated = !parametersToolStripMenuItem.Visible;
            parametersToolStripMenuItem.Visible = true;
            for (int i = 0; i < Information.AP_Names.Count; i++)
            {
                bool exist = false;
                for (int j = 0; j < parametersToolStripMenuItem.DropDownItems.Count; j++)
                {
                    if (parametersToolStripMenuItem.DropDownItems[j].Text == Information.AP_Names[i])
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    ToolStripMenuItem T = new ToolStripMenuItem(Information.AP_Names[i]);
                    parametersToolStripMenuItem.DropDownItems.Add(T);
                    T.Click += new EventHandler(JumpIntoDifferentDimension);
                    if (i >= gp.payoffs.Count)
                    {
                        gp.CreateNewPayoffLayer();
                        gp.TotalDimensions++;
                    }
                    if (gp.TotalDimensions == 1)
                        T.Checked = true;
                }
            }
            if (FirstTimeCreated)
            {
                ToolStripMenuItem T = new ToolStripMenuItem("Total");
                parametersToolStripMenuItem.DropDownItems.Add(T);
                T.Click += new EventHandler(CreateTotalMatrix);
            }
        }

        void CreateTotalMatrix(object sender, EventArgs e)
        {
            for (int i = 0; i < parametersToolStripMenuItem.DropDownItems.Count - 1; i++)
                (parametersToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            (sender as ToolStripMenuItem).Checked = true;

            int pl1 = (int)A.Tag,
                pl2 = (int)B.Tag;

            save_payoff_matrixes_data(pl1, pl2, A.Rows.Count, A.Columns.Count);
            gp.CalculatePayoffsValues();
            List<List<double>> Ua = gp.CreateUtilityPayoffMatrix(pl1, pl2);
            List<List<double>> Ub = gp.CreateUtilityPayoffMatrix(pl2, pl1);
            gp.CurrentDimension = -1;
            
            for (int i = 0; i<gp.Strategies[pl1];i++)
                for (int j = 0; j < gp.Strategies[pl2]; j++)
                {
                    A[j, i].Value = Ua[i][j].ToString("0.00");
                    B[j, i].Value = Ub[i][j].ToString("0.00");
                }
            
            //CreatePayoffGrids((int)A.Tag, (int)B.Tag, calculateValuesToolStripMenuItem.Checked);
            LocationCorrection();
        }



        void JumpIntoDifferentDimension(object sender, EventArgs e)
        {
            string s = (sender as ToolStripDropDownItem).Text;
            int ParameterIndex = 0;
            for (int i = 0; i < Information.AP_Names.Count; i++)
            {
                if (s == Information.AP_Names[i])
                {
                    ParameterIndex = i;
                    break;
                }
            }
            for (int i = 0; i < parametersToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (i != ParameterIndex)
                    (parametersToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }
            (sender as ToolStripMenuItem).Checked = true;
            if (gp.CurrentDimension > 0)
                save_payoff_matrixes_data((int)A.Tag, (int)B.Tag, A.Rows.Count, A.Columns.Count);
            gp.CurrentDimension = ParameterIndex;
            CreatePayoffGrids((int)A.Tag, (int)B.Tag, calculateValuesToolStripMenuItem.Checked);
            LocationCorrection();
        }

        private void calculateValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (payoff_panel.Visible)
            {
                A.EndEdit();
                B.EndEdit();
            }
            if (Information.AP_Names.Count > 0)
            {
                if (calculateValuesToolStripMenuItem.Checked)
                    calculateValuesToolStripMenuItem.Checked = false;
                else
                {
                    save_payoff_matrixes_data((int)A.Tag, (int)B.Tag, gp.Strategies[(int)A.Tag], gp.Strategies[(int)B.Tag]);
                    calculateValuesToolStripMenuItem.Checked = true;
                    gp.CalculatePayoffsValues();
                }
                
                CreatePayoffGrids((int)A.Tag, (int)B.Tag, calculateValuesToolStripMenuItem.Checked);
                LocationCorrection();
            }
            else
                if (System.Windows.Forms.MessageBox.Show("You have not assigned any parameters. Do you want to assign them?",
                    "No parameters are set", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ParametersSettingsForm PSF = new ParametersSettingsForm();
                    PSF.StartPosition = FormStartPosition.CenterScreen;
                    if (PSF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        viewToolStripMenuItem_Click(this, new EventArgs());


                }
        }

    }
}
