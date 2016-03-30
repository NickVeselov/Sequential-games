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
    public partial class ParametersSettingsForm : Form
    {
        public ParametersSettingsForm()
        {
            InitializeComponent();

            ToolTip t = new ToolTip();
            t.SetToolTip(MP_InitTB, "Initial value (at the root of the tree)");
            //TestingInitialization();
            InitAddParamGrid();

            G.Select();
        }

        private void TestingInitialization()
        {
            Information.players_number = 3;
            Information.PlayersNames.Add("Swlabr");
            Information.PlayersNames.Add("Mr.Green");
            Information.PlayersNames.Add("Mr.Orange");
        }

        private void InitAddParamGrid()
        {
            Graphic_Interface.Grid APG = new Graphic_Interface.Grid(G, Math.Max
                (Information.AP_Names.Count, 1), 2, "N", "T");
            APG.initialize();
            G.Columns[0].HeaderText = "Name";
            G.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke; 
            G.Columns[0].DefaultCellStyle.ForeColor = Color.Maroon;
            G.Columns[1].HeaderText = "Variable";
            G.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            G.Columns[1].DefaultCellStyle.ForeColor = Color.Maroon;

            APG.create_headers();

            for (int i = 0; i < G.Rows.Count - 1; i++)
            {
                G[0, i].Value = Information.AP_Names[i + 1];
                G[1, i].Value = Information.AP_KeyLetters[i + 1];
            }


            this.Width = MP_InitTB.Right + 30;
            AdditionalPanel.Width = this.Width;
            this.Height = AdditionalPanel.Top + G.Height + 100;
            AdditionalPanel.Height = G.Height + 100;

        }

//Debug//
        public void G_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((G[e.ColumnIndex, e.RowIndex] != null)&&(e.RowIndex == G.Rows.Count - 1))
            {
                int H = G.Rows[0].Height;

                DataGridViewRow row = new DataGridViewRow();
                DataGridViewCell[] Cells = new DataGridViewCell[2];
                for (int cind = 0; cind < 2; cind++)
                    Cells[cind] = new DataGridViewTextBoxCell();

                row.Cells.AddRange(Cells);

                G.Rows.Add(row);
                G.Rows[G.RowCount - 1].Height = H;
                G.Rows[G.RowCount - 1].HeaderCell.Style.Font = new Font("Bookman Old Style", 12, System.Drawing.FontStyle.Bold);
                G.Rows[G.RowCount - 1].HeaderCell.Value = G.RowCount.ToString();
                G.Rows[G.RowCount - 1].DefaultCellStyle.Alignment =
                                DataGridViewContentAlignment.MiddleCenter;

                G.Height += H;
                AdditionalPanel.Height += H;
                this.Height += H;

                Graphic_Interface.Analyzer.ResizeColumn(this, false, G, e.RowIndex, e.ColumnIndex, G[e.ColumnIndex, e.RowIndex].Value.ToString(), 100);
            }
            if ((e.ColumnIndex == 0) && (G[e.ColumnIndex, e.RowIndex] != null))
                G[1, e.RowIndex].Value = G[e.ColumnIndex, e.RowIndex].Value.ToString().Substring(0, 1);
        }

//Debug//
        public void finishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G.EndEdit();
            bool AllFilled = true;
            for (int i = 0; i < G.Rows.Count - 1; i++)
            {
                if (!AllFilled)
                    break;
                for (int j = 0; j < G.Rows[i].Cells.Count; j++)
                {
                    if (G[j, i].Value == null)
                    {
                        AllFilled = false;
                        break;
                    }
                }
            }

            if (AllFilled)
            {
                bool AllDataIsCorrect = true;
                for (int i = 0; i < G.Rows.Count - 1; i++)
                {
                    if (!AllDataIsCorrect)
                        break;
                    for (int j = 2; j < G.Rows[i].Cells.Count; j++)
                    {
                        if (Graphic_Interface.Analyzer.CheckValidStringDouble(G[j, i].Value.ToString(), 0, 0, true) == "")
                        {
                            AllDataIsCorrect = false;
                            break;
                        }
                    }
                }

                if (AllDataIsCorrect)
                {
                    bool Coincide = false;
                    for (int i = 0; i < G.Rows.Count - 1; i++)
                    {
                        if (!Coincide)
                        {
                            for (int j = i + 1; j < G.Rows.Count - 1; j++)
                                if (G[1, i].Value == G[1, j].Value)
                                {
                                    Coincide = true;
                                    System.Windows.Forms.MessageBox.Show("Key letters should not match: string " + (i + 1).ToString());
                                    break;
                                }
                            if (G[1, i].Value.ToString() == MP_InitTB.Text.Substring
                                (1, MP_InitTB.Text.Length - 2))
                            {
                                Coincide = true;
                                System.Windows.Forms.MessageBox.Show("Key letters should not match: string " + (i + 1).ToString());
                            }
                        }
                        else
                            break;
                    }

                    if (!Coincide)
                    {
                        Information.AP_Names.Clear();
                        Information.AP_KeyLetters.Clear();
                        Information.AP_InitialValues.Clear();

                        Information.AP_Names.Add(MainParamTB.Text);
                        Information.AP_KeyLetters.Add(MP_InitTB.Text.Substring
                            (1, MP_InitTB.Text.Length - 2));
                        Information.AP_InitialValues.Add(new List<double>());

                        for (int i = 0; i < G.Rows.Count - 1; i++)
                        {
                            Information.AP_Names.Add(G[0, i].Value.ToString());
                            Information.AP_KeyLetters.Add(G[1, i].Value.ToString());
                            Information.AP_InitialValues.Add(new List<double>());
                            for (int j = 2; j < G.Rows[i].Cells.Count; j++)
                                Information.AP_InitialValues.Last().Add(Convert.ToDouble(G[j, i].Value));
                        }
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                }
                else
                    System.Windows.Forms.MessageBox.Show("Initial values may only contain numbers","Invalid input");
            }
            else
                System.Windows.Forms.MessageBox.Show("Some cells are empty", "Invalid input");
        }

    }
}
